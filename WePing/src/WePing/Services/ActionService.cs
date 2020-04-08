using MicroS_Common.Types;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using WePing.Actions;
using WePing.domain.ClubDetails.Queries;
using WePing.domain.Clubs.Queries;
using WePing.domain.Equipes.Queries;
using WePing.domain.Joueurs.Queries;
using WePing.domain.Licences.Queries;
using WePing.domain.Organismes.Queries;
using WePing.domain.ResultatEquipeRencontres.Queries;
using WeRedux;


namespace WePing.Services
{

    public interface IActionService
    {
    }
    public class ActionService : IActionService
    {
        #region private variables
        private readonly IStore<WePingState, IAction> Store;
        private readonly NavigationManager Navigator;
        private readonly ISpidService Spid;
        #endregion
        public ActionService(IStore<WePingState, IAction> store, NavigationManager navigator, ISpidService spid)
        {
            this.Store = store;
            this.Navigator = navigator;
            this.Spid = spid;

            #region Navigation
            Store.On<NavigateToHome>().Subscribe(action => navigator.NavigateTo("/"));
            Store.On<NavigateToSearchClub>().Subscribe(action =>
             {
                 navigator.NavigateTo("/searchclub");
             });
            Store.On<NavigateToSearchPlayer>().Subscribe(action =>
            {
                navigator.NavigateTo("/searchPlayer");
            });
            Store.On<NavigateToClub>().Subscribe(action =>
            {
                navigator.NavigateTo($"/club/{((NavigateToClub)action).Numero}");
            });
            Store.On<NavigateToPlayer>().Subscribe(action =>
            {
                navigator.NavigateTo($"/player/{((NavigateToPlayer)action).Licence}");
            });
            #endregion

            #region Action
            Store.On<BrowseClubsAction>().Subscribe(action =>
            {
                var a =action as BrowseClubsAction;
                ExecutePagedRequest(
                    action as BrowseClubsAction,
                    new BrowseClubs()
                    {
                        Code=a.Model.Code,
                        Numero=a.Model.Numero,
                        Ville=a.Model.Ville,
                        Dep=a.Model.Dep,
                        Results = Int32.MaxValue
                    },
                    Spid.GetClubs,
                    res => Store.State.Clubs = res
                );
            });
            Store.On<BrowseJoueursAction>().Subscribe(action =>
            {
                ExecutePagedRequest(
                    action as BrowseJoueursAction,
                    new BrowseJoueur()
                    {
                        Nom = ((BrowseJoueursAction)action).Model.Nom,
                        Prenom= ((BrowseJoueursAction)action).Model.Prenom,
                        Results = Int32.MaxValue
                    },
                    Spid.GetJoueurs,
                    res => Store.State.Joueurs = res
                );
                
            });
            Store.On<GetClubAction>().Subscribe(action =>
            {
                ExecuteRequest(
                    action as GetClubAction,
                    new GetClub()
                    {
                        Numero = ((GetClubAction)action).Numero
                    },
                    spid.GetClub,
                    res => Store.State.Club = res
                    );
            });
            Store.On<BrowseClubDetailAction>().Subscribe(action =>
            {

                ExecuteRequest(
                    action as BrowseClubDetailAction,
                    new GetClubDetail()
                    {
                        Club = ((BrowseClubDetailAction)action).Numero
                    },
                    Spid.GetClubDetail,
                    res => Store.State.ClubDetail = res
                );
            });
            Store.On<BrowseLicencesForClubAction>().Subscribe(action =>
            {

                ExecutePagedRequest(
                    action as BrowseLicencesForClubAction,
                    new BrowseLicences()
                    {
                        Club = ((BrowseLicencesForClubAction)action).Numero,
                        Results = Int32.MaxValue
                    },
                    Spid.GetLicencesForClub,
                    res => Store.State.LicencesForClubs = res
                );
            });
            Store.On<BrowseEquipeAction>().Subscribe(action =>
            {
                var a = action as BrowseEquipeAction;
                ExecutePagedRequest(
                    a,
                    new BrowseEquipes()
                    {
                        NumClu = ((BrowseEquipeAction)action).Numero
                    },
                Spid.GetEquipes,
                res =>
                {
                    Store.State.Equipes = res;
                    res.Items.ForEach(equipe => equipe.NumeroClub = a.Numero);
                }
                );
            });
            Store.On<BrowseOrganismesAction>().Subscribe(action =>
            {
                ExecutePagedRequest(
                    action as BrowseOrganismesAction,
                    new BrowseOrganismes() { Results = Int32.MaxValue },
                    Spid.GetOrganismes,
                    res => Store.State.Organismes = res
                    );
            });
            Store.On<BrowseResultatEquipeClassementAction>().Subscribe(action =>
            {
                var a = action as BrowseResultatEquipeClassementAction;

                ExecutePagedRequest(
                    action as BrowseResultatEquipeClassementAction,
                new BrowseResultatEquipeClassements()
                {
                    Cx_poule = ((BrowseResultatEquipeClassementAction)action).Cx_poule,
                    D1 = ((BrowseResultatEquipeClassementAction)action).D1
                },
                Spid.GetResultatEquipeClassements,
                res =>
                {
                    a.Equipe.Classements = res.Items;
                    for (int i = 1; i < a.Equipe.Classements.Count; i++)
                    {
                        if (!Int32.TryParse(a.Equipe.Classements[i].Classement, out var clt))
                        {
                            var idx = i - 1;
                            bool ok = false;
                            while (idx >= 0)
                            {
                                if(Int32.TryParse(a.Equipe.Classements[idx].Classement, out var clt1))
                                {
                                    a.Equipe.Classements[i].Classement = clt1.ToString();
                                    ok = true;
                                    break;
                                }
                                idx -= 1;
                            }
                            if (!ok)
                                a.Equipe.Classements[i].Classement = "0";

                        }
                    }
                    a.Equipe.Classement = a.Equipe.GetClassement(a.Equipe.NumeroClub);
                }
                );
            });
            #endregion
        }


        #region private Methods
        private void ExecuteRequest<TAction, TQuery, TDto>(TAction action, TQuery query, Func<TQuery, Task<TDto>> fn, Action<TDto> result)
            where TAction : IAction
            where TQuery : IQuery<TDto>
        {
            Store.State.IsLoading = false;
            var t = Task.Run(async delegate
            {
                var req = fn(query);
                result(await req);
                return;
            });

            t.Wait();
            Store.State.IsLoading = false;
            Store.StateChanged<TAction>(action);
        }

        private void ExecutePagedRequest<TAction, TQuery, TDto>(TAction action, TQuery query, Func<TQuery, Task<TDto>> fn, Action<TDto> result)
            where TAction : IAction

        {
            Store.State.IsLoading = false;
            try
            {
                var t = Task.Run(async delegate
                {
                    var req = fn(query);
                    result(await req);
                    return;
                });

                t.Wait();
            }
            catch { }
            Store.State.IsLoading = false;
            Store.StateChanged<TAction>(action);
        }
        #endregion
    }

}


using MicroS_Common.Actions;
using MicroS_Common.Types;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WeCommon;
using WePing.Actions;
using WePing.Data;
using WePing.domain.ClubDetails.Queries;
using WePing.domain.Clubs.Queries;
using WePing.domain.Divisions.Queries;
using WePing.domain.Epreuves.Queries;
using WePing.domain.Equipes.Queries;
using WePing.domain.HistoriqueClassements.Queries;
using WePing.domain.JoueurDetails.Queries;
using WePing.domain.Joueurs.Queries;
using WePing.domain.Licences.Queries;
using WePing.domain.Organismes.Queries;
using WePing.domain.Parties.Queries;
using WePing.domain.Rencontres.Queries;
using WePing.domain.ResultatEquipeRencontres.Dto;
using WePing.domain.ResultatEquipeRencontres.Queries;
using WeRedux;
using WeReduxBlazor;
namespace WePing.Services
{

    public interface IActionService
    {
        bool AllowStateChanged { get; set; }
        void NotifyStateChanged<TAction>(TAction action)
             where TAction : IAction;
        void NotifyStateChanged();
        IObservable<LocationChangedEventArgs> OnLocationChanged { get; }

        WeQueryString QueryString { get; }
    }
    public class ActionService : IActionService, IDisposable
    {
        #region private variables
        private readonly ILogger<ActionService> Logger;
        private readonly IStore<WePingState, IAction> Store;
        private readonly NavigationManager Navigator;
        private readonly ISpidService Spid;
        private readonly LocalStorage Storage;
        private readonly IDataProtector Protector;
        private const string PURPOSE = @"AqGgLcLBwUkXJy22jLHQfcBTpNdqwE5HZjqRbsAsJmKbt3JMVHfhWkrgM2sF9DwMT26JGWkvYG4WvhAF8sY7tgzmhcdVhT22HxBkEsvj8QkzCDepdEDMjRhG9hMtpARn";
        private readonly List<IDisposable> Disposables = new List<IDisposable>();
        #endregion
        public ActionService(ILogger<ActionService> logger, IStore<WePingState, IAction> store, NavigationManager navigator, ISpidService spid, LocalStorage storage, IDataProtectionProvider protectionProvider)
        {
            this.Logger = logger;
            this.Store = store;
            this.Navigator = navigator;
            this.Spid = spid;
            this.Storage = storage;
            this.Protector = protectionProvider.CreateProtector(PURPOSE); ;

            Logger.LogInformation("ActionService CTor");

            #region Navigation
            OnLocationChanged.Subscribe(e =>
            {
                logger.LogInformation($"************************************************");
                logger.LogInformation($"LocationChanged to {e.Location}=>reset the Store");
                var profile = store.State.Profile;
                store.Reset();
                store.State.Profile = profile;
            })
                .AddDisposable(Disposables); ;

            Store.On<NavigateToHome>().Subscribe(action =>
            {
                logger.LogInformation($"NavigateToHome");
                navigator.NavigateTo("/");
            }).AddDisposable(Disposables);
            Store.On<NavigateToSearchClub>().Subscribe(action =>
             {
                 logger.LogInformation($"NavigateToSearchClub");
                 navigator.NavigateTo("/searchclub");
             }).AddDisposable(Disposables);
            Store.On<NavigateToSearchPlayer>().Subscribe(action =>
            {
                logger.LogInformation($"NavigateToSearchPlayer");
                navigator.NavigateTo("/searchPlayer");
            }).AddDisposable(Disposables);
            Store.On<NavigateToClub>().Subscribe(action =>
            {
                navigator.NavigateTo($"/club/{((NavigateToClub)action).Numero}");
            }).AddDisposable(Disposables);
            Store.On<NavigateToPlayer>().Subscribe(action =>
            {
                logger.LogInformation($"NavigateToPlayer");
                navigator.NavigateTo($"/player/{((NavigateToPlayer)action).Licence}");
            }).AddDisposable(Disposables);
            Store.On<NavigateToClassementEquipe>().Subscribe(action =>
            {
                var a = (NavigateToClassementEquipe)action;
                logger.LogInformation($"NavigateToClassementEquipe");
                navigator.NavigateTo($"/clte/{a.NumeroClub}/{a.Division}/{a.Poule}");
            }).AddDisposable(Disposables);
            Store.On<NavigateToDetailRencontre>().Subscribe(action=> {
                var a = (NavigateToDetailRencontre)action;
                logger.LogInformation($"NavigateToDetailRencontre");
                navigator.NavigateTo($"/detailrencontre?{a.Lien}");
            }).AddDisposable(Disposables);

            #endregion

            #region Action
            
            Store.On<BrowseDivisionAction>().Subscribe(action =>
            {
                logger.LogInformation($"BrowseDivisionAction");
                var a = action as BrowseDivisionAction;
                ExecutePagedRequest(
                    a,
                    new BrowseDivisions()
                    {
                        Epreuve=a.Epreuve,
                        Organisme=a.Organisme,
                        Results = Int32.MaxValue
                    },
                    Spid.GetDivisions,
                    res => Store.State.Divisions = res
                );
            }).AddDisposable(Disposables);
            Store.On<BrowseEpreuvesAction>().Subscribe(action =>
            {
                logger.LogInformation($"BrowseEpreuvesAction");
                var a = action as BrowseEpreuvesAction;
                ExecutePagedRequest(
                    a,
                    new BrowseEpreuves()
                    {
                        Organisme=a.Organisme,
                        Results = Int32.MaxValue
                    },
                    Spid.GetEpreuves,
                    res => Store.State.Epreuves = res
                );
            }).AddDisposable(Disposables);
            
            Store.On<BrowseClubsAction>().Subscribe(action =>
            {
                logger.LogInformation($"BrowseClubsAction");
                var a = action as BrowseClubsAction;
                ExecutePagedRequest(
                   a,
                    new BrowseClubs()
                    {
                        Code = a.Model.Code,
                        Numero = a.Model.Numero,
                        Ville = a.Model.Ville,
                        Dep = a.Model.Dep,
                        Results = Int32.MaxValue
                    },
                    Spid.GetClubs,
                    res => Store.State.Clubs = res
                );
            }).AddDisposable(Disposables);
            Store.On<BrowseJoueursAction>().Subscribe(action =>
            {
                logger.LogInformation($"BrowseJoueursAction");
                ExecutePagedRequest(
                    action as BrowseJoueursAction,
                    new BrowseJoueur()
                    {
                        Nom = ((BrowseJoueursAction)action).Model.Nom,
                        Prenom = ((BrowseJoueursAction)action).Model.Prenom,
                        Results = Int32.MaxValue
                    },
                    Spid.GetJoueurs,
                    res => Store.State.Joueurs = res
                );

            }).AddDisposable(Disposables);

            Store.On<BrowseHistoriqueClassementAction>().Subscribe(action =>
            {
                logger.LogInformation($"BrowseHistoriqueClassementAction");
                var a = action as BrowseHistoriqueClassementAction;
                ExecutePagedRequest(
                    a,
                    new BrowseHistoriqueClassements()
                    {
                        NumLic = a.Licence,
                        Results = Int32.MaxValue
                    },
                    Spid.GetHistoriqueClassement,
                    res => Store.State.HistoriqueClassement = res
                );
            }).AddDisposable(Disposables);
            Store.On<GetRencontreAction>().Subscribe(action => {
                logger.LogInformation($"BrowseRencontreAction");
                var a = (GetRencontreAction)action;
                ExecuteRequest(
                    a,
                    new GetRencontreLien()
                    {
                        Lien=a.Lien
                    },
                    Spid.GetRencontre,
                    res => Store.State.Rencontre = res
                );
            }).AddDisposable(Disposables);
            Store.On<GetClubAction>().Subscribe(action =>
            {
                logger.LogInformation($"GetClubAction");
                ExecuteRequest(
                    action as GetClubAction,
                    new GetClub()
                    {
                        Numero = ((GetClubAction)action).Numero
                    },
                    spid.GetClub,
                    res => Store.State.Club = res
                    );
            }).AddDisposable(Disposables);
            Store.On<GetLicenceAction>().Subscribe(action =>
            {
                logger.LogInformation($"GetLicenceAction");
                ExecuteRequest(
                    action as GetLicenceAction,
                    new GetLicence()
                    {
                        Licence = ((GetLicenceAction)action).Licence
                    },
                    spid.GetLicence,
                    res => Store.State.Licence = res
                );
            }).AddDisposable(Disposables);
            Store.On<GetJoueurAction>().Subscribe(action =>
            {
                logger.LogInformation($"GetJoueurAction");
                ExecuteRequest(
                    action as GetJoueurAction,
                    new GetJoueurDetail()
                    {
                        Licence = ((GetJoueurAction)action).Licence
                    },
                    spid.GetJoueurDetail,
                    res => Store.State.Joueur = res
                );
            }).AddDisposable(Disposables);
            Store.On<BrowseClubDetailAction>().Subscribe(action =>
            {
                logger.LogInformation($"BrowseClubDetailAction");
                ExecuteRequest(
                    action as BrowseClubDetailAction,
                    new GetClubDetail()
                    {
                        Club = ((BrowseClubDetailAction)action).Numero
                    },
                    Spid.GetClubDetail,
                    res => Store.State.ClubDetail = res
                );
            }).AddDisposable(Disposables);
            Store.On<BrowseLicencesForClubAction>().Subscribe(action =>
            {
                logger.LogInformation($"BrowseLicencesForClubAction");
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
            }).AddDisposable(Disposables);
            Store.On<BrowseEquipeAction>().Subscribe(action =>
            {
                logger.LogInformation($"BrowseEquipeAction");
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
            }).AddDisposable(Disposables);
            Store.On<BrowseOrganismesAction>().Subscribe(action =>
            {
                logger.LogInformation($"BrowseOrganismesAction");
                ExecutePagedRequest(
                    action as BrowseOrganismesAction,
                    new BrowseOrganismes() { Results = Int32.MaxValue },
                    Spid.GetOrganismes,
                    res => Store.State.Organismes = res
                    );
            }).AddDisposable(Disposables);
            Store.On<BrowseResultatEquipeClassementAction>().Subscribe(action =>
            {
                logger.LogInformation($"BrowseResultatEquipeClassementAction");
                var a = action as BrowseResultatEquipeClassementAction;

                ExecutePagedRequest(
                    action as BrowseResultatEquipeClassementAction,
                new BrowseResultatEquipeClassements()
                {
                    Cx_poule = a.Poule,
                    D1 = a.Division
                },
                Spid.GetResultatEquipeClassements,
                res =>
                {
                    var equipe = Store.State.Equipes.Items.Where(e => e.GetPouleId() == a.Poule && e.GetDivision() == a.Division).FirstOrDefault();
                    if (equipe != null)
                    {
                        equipe.Classements = new List<ResultatEquipeClassementDto>();
                        equipe.Classements.AddRange(res.Items);
                        for (int i = 1; i < equipe.Classements.Count; i++)
                        {
                            if (!Int32.TryParse(equipe.Classements[i].Classement, out var clt))
                            {
                                var idx = i - 1;
                                bool ok = false;
                                while (idx >= 0)
                                {
                                    if (Int32.TryParse(equipe.Classements[idx].Classement, out var clt1))
                                    {
                                        equipe.Classements[i].Classement = clt1.ToString();
                                        ok = true;
                                        break;
                                    }
                                    idx -= 1;
                                }
                                if (!ok)
                                    equipe.Classements[i].Classement = "0";

                            }
                        }
                        equipe.Classement = equipe.GetClassement(equipe.NumeroClub);
                    }
                }
                );
            }).AddDisposable(Disposables);
            Store.On<BrowseResultatEquipeRencontresAction>().Subscribe(action =>
            {
                logger.LogInformation($"BrowseResultatEquipeRencontresAction");
                var a = (BrowseResultatEquipeRencontresAction)action;
                ExecutePagedRequest(
                    action as BrowseResultatEquipeRencontresAction,
                    new BrowseResultatEquipeRencontres()
                    {
                        Results = Int32.MaxValue,
                        D1 = a.Division,
                        Cx_poule = a.Poule
                    },
                    Spid.GetResultatEquipeRencontres,
                    res =>
                    {
                        var equipe = Store.State.Equipes.Items.Where(e => e.GetPouleId() == a.Poule && e.GetDivision() == a.Division).FirstOrDefault();
                        if (equipe != null)
                        {
                            equipe.Rencontres = res.Items;
                        }
                    });
            }).AddDisposable(Disposables);
            Store.On<BrowsePartiesAction>().Subscribe(action =>
            {
                logger.LogInformation($"BrowsePartiesAction");
                ExecutePagedRequest(
                    action as BrowsePartiesAction,
                    new BrowseParties()
                    {
                        Licence = ((BrowsePartiesAction)action).Licence,
                        Results = Int32.MaxValue
                    },
                    Spid.GetParties,
                    res => Store.State.Parties = res
                );
            }).AddDisposable(Disposables);
            #endregion

            #region Profile
            ProtecteProfileKey = false;

            Store.On<SetMyProfileAction>().Subscribe(async (action) =>
            {
                logger.LogInformation($"SetMyProfileAction");

                var a = action as SetMyProfileAction;
                var value = Protector.Protect(JsonSerializer.Serialize(new MyProfile() { NumeroLicence = a.Licence }));
                var key = GetProfileKey();// Protector.Protect("WePing:Profile");
                await storage.SetItemAsync(key, value);


                /*  var _value = await storage.GetItemAsync<string>(key);
                  var decoded = Protector.Unprotect(_value);
                  var profile = JsonSerializer.Deserialize<MyProfile>(decoded);*/
                await Store.DispatchAsync<GetMyProfileAction>();
            }).AddDisposable(Disposables);
            Store.On<SaveMyProfileAction>().Subscribe(async (action) =>
            {
                logger.LogInformation($"SaveMyProfileAction");
                var a = action as SaveMyProfileAction;
                var value = Protector.Protect(JsonSerializer.Serialize(store.State.Profile));
                var key = Protector.Protect("WePing:Profile");
                await storage.SetItemAsync(key, value);
            }).AddDisposable(Disposables);
            Store.On<GetMyProfileAction>().Subscribe(async (action) =>
            {
                logger.LogInformation($"GetMyProfileAction");
                var key = GetProfileKey();
                var value = await storage.GetItemAsync<string>(key);
                if (!string.IsNullOrEmpty(value))
                {
                    var decoded = Protector.Unprotect(value);
                    Store.State.Profile = JsonSerializer.Deserialize<MyProfile>(decoded);
                    Store.StateChanged(action as GetMyProfileAction);
                }
            }).AddDisposable(Disposables);

            Store.On<ClearProfileAction>().Subscribe(async (action) =>
            {
                logger.LogInformation($"ClearProfileAction");
                var key = Protector.Protect("WePing:Profile");
                await storage.RemoveItemAsync(key);
                Store.StateChanged(action as ClearProfileAction);
            }).AddDisposable(Disposables);

            Store.OnChanged.Where(mutation => mutation.Action == typeof(GetMyProfileAction)).Subscribe(mutation =>
            {
                logger.LogInformation($"StateChanged where action=GetMyProfileAction");
                Store.Dispatch<GetProfileLicenceAction>(a => a.Licence = mutation.State.Profile.NumeroLicence);
                Store.Dispatch<GetProfileJoueurAction>(a => a.Licence = mutation.State.Profile.NumeroLicence);
            }).AddDisposable(Disposables);

            Store.On<GetProfileLicenceAction>().Subscribe(action =>
            {
                logger.LogInformation($"GetProfileLicenceAction");
                ExecuteRequest(
                    action as GetProfileLicenceAction,
                    new GetLicence()
                    {
                        Licence = ((GetProfileLicenceAction)action).Licence
                    },
                    spid.GetLicence,
                    res => Store.State.Profile.Licence = res
                );
            }).AddDisposable(Disposables);
            Store.On<GetProfileJoueurAction>().Subscribe(action =>
            {
                logger.LogInformation($"GetProfileJoueurAction");
                ExecuteRequest(
                    action as GetProfileJoueurAction,
                    new GetJoueurDetail()
                    {
                        Licence = ((GetProfileJoueurAction)action).Licence
                    },
                    spid.GetJoueurDetail,
                    res => Store.State.Profile.Joueur = res
                );
            }).AddDisposable(Disposables);

            #endregion
        }


        public bool ProtecteProfileKey { get; set; } = true;
        private string GetProfileKey()
        {
            var res = "WePing:Profile";
            if (ProtecteProfileKey)
                res = Protector.Protect(res);
            return res;
        }
        public IObservable<LocationChangedEventArgs> OnLocationChanged
            =>
            Observable.FromEventPattern<LocationChangedEventArgs>(
                e => Navigator.LocationChanged += e,
                e => Navigator.LocationChanged -= e
                ).Select(x => x.EventArgs);

        #region private Methods
        public bool AllowStateChanged { get; set; } = true;

        public WeQueryString QueryString =>
              WeQueryString.Create(Navigator.ToAbsoluteUri(Navigator.Uri).Query);

        private object _lastAction;

        private TAction getLastAction<TAction>() where TAction : IAction
        {
            return (TAction)_lastAction;
        }
        public void NotifyStateChanged<TAction>(TAction action)
             where TAction : IAction
        {
            _lastAction = action;
            if (AllowStateChanged)
                Store.StateChanged<TAction>(action);
        }
        public void NotifyStateChanged()
        {
            if (AllowStateChanged)
                Store.StateChanged(getLastAction<IAction>());
        }
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
            NotifyStateChanged(action);
            // Store.StateChanged<TAction>(action);
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
            NotifyStateChanged(action);
            // Store.StateChanged<TAction>(action);
        }

        #region IDisposable Support
        private bool disposedValue = false; // Pour détecter les appels redondants

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: supprimer l'état managé (objets managés).
                    foreach (var item in Disposables)
                    {
                        item.Dispose();
                    }
                }

                // TODO: libérer les ressources non managées (objets non managés) et remplacer un finaliseur ci-dessous.
                // TODO: définir les champs de grande taille avec la valeur Null.
                Logger.LogDebug("Actionservice disposing");
                disposedValue = true;
            }
        }

        // TODO: remplacer un finaliseur seulement si la fonction Dispose(bool disposing) ci-dessus a du code pour libérer les ressources non managées.
        // ~ActionService()
        // {
        //   // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
        //   Dispose(false);
        // }

        // Ce code est ajouté pour implémenter correctement le modèle supprimable.
        public void Dispose()
        {
            // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
            Dispose(true);
            // TODO: supprimer les marques de commentaire pour la ligne suivante si le finaliseur est remplacé ci-dessus.
            // GC.SuppressFinalize(this);
        }
        #endregion
        #endregion
    }

}


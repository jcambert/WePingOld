
using MicroS_Common;
using MicroS_Common.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using WePing.domain;
using WePing.domain.ActuFftts.Domain;
using WePing.domain.ActuFftts.Queries;
using WePing.domain.ClassementJoueurs.Domain;
using WePing.domain.ClassementJoueurs.Queries;
using WePing.domain.ClubDetails.Domain;
using WePing.domain.ClubDetails.Queries;
using WePing.domain.Clubs.Domain;
using WePing.domain.Clubs.Queries;
using WePing.domain.Divisions.Domain;
using WePing.domain.Divisions.Queries;
using WePing.domain.Epreuves.Domain;
using WePing.domain.Epreuves.Queries;
using WePing.domain.Equipes.Domain;
using WePing.domain.Equipes.Queries;
using WePing.domain.HistoriqueClassements.Domain;
using WePing.domain.HistoriqueClassements.Queries;
using WePing.domain.JoueurDetails.Domain;
using WePing.domain.JoueurDetails.Queries;
using WePing.domain.Joueurs.Domain;
using WePing.domain.Joueurs.Queries;
using WePing.domain.Licences.Domain;
using WePing.domain.Licences.Queries;
using WePing.domain.Organismes.Domain;
using WePing.domain.Organismes.Queries;
using WePing.domain.Parties.Domain;
using WePing.domain.Parties.Queries;
using WePing.domain.ResultatEquipeRencontres.Domain;
using WePing.domain.ResultatEquipeRencontres.Queries;
using WePing.domain.ResultatIndividuels.Domain;
using WePing.domain.ResultatIndividuels.Queries;
using WePing.domain.Services;

namespace WePing.Service.Spid.Services
{



    public interface ISpidRequest
    {
        Task<List<Club>> GetClubs(BrowseClubs query);
        Task<ClubDetail> GetClubDetails(GetClubDetail query);
        Task<List<Organisme>> GetOrganismes(BrowseOrganismes query);
        Task<List<Epreuve>> GetEpreuves(BrowseEpreuves query);
        Task<List<Division>> GetDivisions(BrowseDivisions query);
        Task<List<ResultatEquipeRencontre>> GetResultatEquipeRencontres(BrowseResultatEquipeRencontres query);
        Task<List<ResultatEquipePoule>> GetResultatEquipePoules(BrowseResultatEquipePoules query);
        Task<List<ResultatEquipeClassement>> GetResultatEquipeClassements(BrowseResultatEquipeClassements query);
        Task<List<Equipe>> GetEquipes(BrowseEquipes query);
        Task<List<ResultatIndividuelPoule>> GetResultatIndividuelPoules(BrowseResultatIndividuelPoules query);
        Task<List<ResultatIndividuelClassement>> GetResultatIndividuelClassements(BrowseResultatIndividuelClassement query);
        Task<List<ResultatIndividuelPartie>> GetResultatIndividuelParties(BrowseResultatIndividuelPartie query);
        Task<List<ClassementJoueur>> GetClassementJoueurs(BrowseClassementJoueurs query);
        Task<List<Joueur>> GetJoueurs(BrowseJoueur query);
        Task<JoueurDetail> GetJoueurDetails(GetJoueurDetail query);
        Task<Licence> GetLicence(GetLicence query);
        Task<List<Licence>> GetLicences(BrowseLicences query);
        Task<List<Partie>> GetParties(BrowseParties query);
        Task<List<ActuFftt>> GetActus(BrowseActuFftts query);
        Task<List<HistoriqueClassement>> GetHistoriqueClassements(BrowseHistoriqueClassements query);
    }
    public sealed class SpidRequest : ISpidRequest
    {
        private readonly SpidOptions _options;
        private readonly HttpClient _client;
        private readonly SpidRequester _requester;
        private readonly ILogger<SpidRequest> _logger;
        private readonly ICalculateurPoints _calculateur;

        public SpidRequest(/*IConfiguration config,*/ SpidRequester requester, SpidOptions options, ILogger<SpidRequest> logger, ICalculateurPoints calculateur/*,HttpClient client*/)
        {

            this._options = options;
            this._client = new HttpClient
            {
                BaseAddress = new Uri(this._options.EndPoint)
            };// client;
            this._requester = requester;
            this._logger = logger;
            this._calculateur = calculateur;
        }

        private async Task<byte[]> Execute<TQuery>(TQuery query, string api_endpoint)
        //where TQuery: PagedQueryBase
        {
            var opt = query.ToDictionnary();
            var url = _options[api_endpoint] + _requester.GetParameters(opt);
            _logger.LogInformation($"Request Spid:{url}");
            var resp = await _client.GetAsync(url);
            return await resp.Content.ReadAsByteArrayAsync();

        }

        public async Task<ClubDetail> GetClubDetails(GetClubDetail query)
        {
            var data = await Execute(query, SpidOptions.CLUB_DETAIL);
            return data.Deserialize<domain.ClubDetails.Domain.ListeClubdetails>().Club;
        }

        public async Task<List<Club>> GetClubs(BrowseClubs query)
        {
            var data = await Execute(query, SpidOptions.CLUB_LISTE);
            return data.Deserialize<domain.Clubs.Domain.ListeClubs>().Clubs;
        }

        public async Task<List<Division>> GetDivisions(BrowseDivisions query)
        {
            var data = await Execute(query, SpidOptions.DIVISION);
            return data.Deserialize<ListeDivisions>().Divisions;
        }

        public async Task<List<Epreuve>> GetEpreuves(BrowseEpreuves query)
        {

            var data = await Execute(query, SpidOptions.EPREUVES);
            return data.Deserialize<ListeEpreuve>().Epreuves;
        }

        public async Task<List<Organisme>> GetOrganismes(BrowseOrganismes query)
        {
            var data = await Execute(query, SpidOptions.ORGANISMES);
            return data.Deserialize<ListeOrganismes>().Organismes;
        }

        public async Task<List<ResultatEquipeRencontre>> GetResultatEquipeRencontres(BrowseResultatEquipeRencontres query)
        {
            var data = await Execute(query, SpidOptions.RESULTAT_EQUIPE_RENCONTRES);
            return data.Deserialize<ListeResultatEquipeRencontres>().Rencontres;
        }

        public async Task<List<ResultatEquipePoule>> GetResultatEquipePoules(BrowseResultatEquipePoules query)
        {
            var data = await Execute(query, SpidOptions.RESULTAT_EQUIPE_POULES);
            return data.Deserialize<ListeResultatEquipePoules>().Poules;
        }

        public async Task<List<ResultatEquipeClassement>> GetResultatEquipeClassements(BrowseResultatEquipeClassements query)
        {
            var data = await Execute(query, SpidOptions.RESULTAT_EQUIPE_CLASSEMENTS);
            return data.Deserialize<ListeResultatEquipeClassements>().Classements;
        }

        public async Task<List<Equipe>> GetEquipes(BrowseEquipes query)
        {
            var data = await Execute(query, SpidOptions.EQUIPES);
            return data.Deserialize<ListeEquipes>().Equipes;
        }

        public async Task<List<ResultatIndividuelPoule>> GetResultatIndividuelPoules(BrowseResultatIndividuelPoules query)
        {
            var data = await Execute(query, SpidOptions.RESULTAT_INDIVIDUEL_POULES);
            return data.Deserialize<ListeResultatIndividuelPoule>().Tours;
        }

        public async Task<List<ResultatIndividuelClassement>> GetResultatIndividuelClassements(BrowseResultatIndividuelClassement query)
        {
            var data = await Execute(query, SpidOptions.RESULTAT_INDIVIDUEL_CLASSEMENTS);
            return data.Deserialize<ListeResultatIndividuelClassement>().Classements;
        }

        public async Task<List<ResultatIndividuelPartie>> GetResultatIndividuelParties(BrowseResultatIndividuelPartie query)
        {
            var data = await Execute(query, SpidOptions.RESULTAT_INDIVIDUEL_PARTIES);
            return data.Deserialize<ListeResultatIndividuelPartie>().Parties;
        }

        public async Task<List<ClassementJoueur>> GetClassementJoueurs(BrowseClassementJoueurs query)
        {
            var data = await Execute(query, SpidOptions.CLASSEMENT_JOUEUR);
            return data.Deserialize<ListeClassementJoueur>().Classements;
        }

        public async Task<List<Joueur>> GetJoueurs(BrowseJoueur query)
        {
            var data = await Execute(query, SpidOptions.JOUEURS);
            return data.Deserialize<ListeJoueurs>().Joueurs;
        }

        public async Task<JoueurDetail> GetJoueurDetails(GetJoueurDetail query)
        {
            var data = await Execute(query, SpidOptions.JOUEUR_DETAIL);
            return data.Deserialize<ListeJoueurDetails>().Joueur;
        }

        public async Task<Licence> GetLicence(GetLicence query)
        {
            var data = await Execute(query, SpidOptions.LICENCE);
            return data.Deserialize<ListeLicences>().Licences.First();
        }



        public async Task<List<Licence>> GetLicences(BrowseLicences query)
        {
            var data = await Execute(query, SpidOptions.LICENCE);
            return data.Deserialize<ListeLicences>().Licences;
        }

        public async Task<List<Partie>> GetParties(BrowseParties query)
        {
            var licence_data = await Execute(new GetLicence() { Licence = query.Licence }, SpidOptions.LICENCE);
            var licence = licence_data.Deserialize<ListeLicences>().Licences.First();

            var data = await Execute(query, SpidOptions.PARTIES);
            var data_ = await Execute(query, SpidOptions.PARTIES_);
            List<Partie> p1 = data.Deserialize<ListeParties>().Parties;
            //List<Partie> p1 = new List<Partie>();
            List<Partie> p2 = data_.Deserialize<ListeParties_>().Parties;
            //List<Partie> p2 = new List<Partie>();
            //var p3 = p2.Where(p_ => !p1.Any(p => p.Date == p_.Date && p.NomPrenomAdversaire == p_.NomPrenomAdversaire_));
            /*var p4 = p1.GroupJoin(p2, a => a.IdPartie, b => b.IdPartie, (a, b) =>
                     {
                         a.Epreuve = b.Epreuve;
                         return a;
                     });*/
            //p1.AddRange(p3);
            p1.ForEach(p =>
            {
                try
                {
                    p.Licence ??= query.Licence;
                    p.PointsMensuel ??= licence.PointsMensuel;
                    p.Points ??= licence.Point;
                    var b0 = double.TryParse(p.PointsMensuel,NumberStyles.Any,CultureInfo.InvariantCulture, out var pointsMensuel);
                    if(!b0)Debugger.Break();
                    var b1 = double.TryParse(p.ClassementAdversaire_ ?? p.ClassementAdversaire, NumberStyles.Any, CultureInfo.InvariantCulture, out var cltAdv);
                    if (!b1) Debugger.Break();
                    p.PointsGagnesPerdus ??= _calculateur.Calculate(pointsMensuel, cltAdv, ((VictoireDefaite)Enum.Parse(typeof(VictoireDefaite), p.VictoireDefaite_))).ToString();
                    p.Epreuve = p2.Where(_p => _p.IdPartie == p.IdPartie).FirstOrDefault()?.Epreuve ?? "";
                }catch(Exception ex)
                {
                    Debugger.Break();
                }
            });
            return p1;
        }

        public async Task<List<ActuFftt>> GetActus(BrowseActuFftts query)
        {
            var data = await Execute(query, SpidOptions.ACTUALITES);
            return data.Deserialize<ListeActualites>().Actualites;
        }

        public async Task<List<HistoriqueClassement>> GetHistoriqueClassements(BrowseHistoriqueClassements query)
        {
            var data = await Execute(query, SpidOptions.HISTO_CLASSEMENT);
            return data.Deserialize<ListeHistoriqueClassements>().Historiques;
        }
    }
    internal static class Extensions
    {
        private static Dictionary<Type, Func<string, string>> _stringConverter = new Dictionary<Type, Func<string, string>>()
        {
            {typeof(UpperCaseAttribute),new UpperCaseAttribute().Convert },
            {typeof(LowerCaseAttribute),new LowerCaseAttribute().Convert }
        };
        public static Dictionary<string, string> ToDictionnary(this Object o)
        {
            var result = new Dictionary<string, string>();
            var props = o.GetType().GetProperties();
            props.ToList().ForEach(p =>
            {
#if DEBUG
                
               // if (p.GetCustomAttribute<UpperCaseAttribute>() != null)
                //    Debugger.Break();
#endif
                var type=p.CustomAttributes.Where(attr=>typeof(StringConverterAttribute).IsAssignableFrom(attr.AttributeType)).FirstOrDefault()?.AttributeType  ?? typeof(LowerCaseAttribute);
                string key = _stringConverter[type](p.Name);
                string value = p.GetGetMethod().Invoke(o, null)?.ToString();
                if (value != null)
                    result[key] = value;
            });
            return result;
        }
    }
}

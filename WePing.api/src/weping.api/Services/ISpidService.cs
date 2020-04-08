using MicroS_Common.Types;
using Microsoft.AspNetCore.Mvc;
using RestEase;
using System.Threading.Tasks;
using WePing.domain.ActuFftts.Dto;
using WePing.domain.ActuFftts.Queries;
using WePing.domain.ClassementJoueurs.Dto;
using WePing.domain.ClassementJoueurs.Queries;
using WePing.domain.ClubDetails.Dto;
using WePing.domain.Clubs.Dto;
using WePing.domain.Clubs.Queries;
using WePing.domain.Divisions.Dto;
using WePing.domain.Divisions.Queries;
using WePing.domain.Epreuves.Dto;
using WePing.domain.Epreuves.Queries;
using WePing.domain.Equipes.Dto;
using WePing.domain.Equipes.Queries;
using WePing.domain.HistoriqueClassements.Dto;
using WePing.domain.HistoriqueClassements.Queries;
using WePing.domain.JoueurDetails.Dto;
using WePing.domain.Joueurs.Dto;
using WePing.domain.Joueurs.Queries;
using WePing.domain.Licences.Dto;
using WePing.domain.Licences.Queries;
using WePing.domain.Organismes.Dto;
using WePing.domain.Organismes.Queries;
using WePing.domain.Parties.Dto;
using WePing.domain.Parties.Queries;
using WePing.domain.Rencontres.Dto;
using WePing.domain.Rencontres.Queries;
using WePing.domain.ResultatEquipeRencontres.Dto;
using WePing.domain.ResultatEquipeRencontres.Queries;
using WePing.domain.ResultatIndividuels.Dto;
using WePing.domain.ResultatIndividuels.Queries;

namespace weping.api.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface ISpidService
    {

        [Get("clubs")]
        [AllowAnyStatusCode]
        public Task<PagedResult<ClubDto>> GetClubs([Query] BrowseClubs query);

        [AllowAnyStatusCode]
        [Get("club/{club}")]
        public Task<ClubDetailDto> GetClubDetail([Path] string club);
        [AllowAnyStatusCode]
        [Get("organismes")]
        public Task<PagedResult<OrganismeDto>> GetOrganismes([FromQuery] BrowseOrganismes query);
        [AllowAnyStatusCode]
        [Get("epreuves")]
        public Task<PagedResult<EpreuveDto>> GetEpreuves([FromQuery] BrowseEpreuves query);
        [AllowAnyStatusCode]
        [Get("divisions")]
        public Task<PagedResult<DivisionDto>> GetDivisions([FromQuery] BrowseDivisions query);
        [AllowAnyStatusCode]
        [Get("resultat_equipe_rencontre")]
        public Task<PagedResult<ResultatEquipeRencontreDto>> GetResultatEquipeRencontre([FromQuery] BrowseResultatEquipeRencontres query);
        [AllowAnyStatusCode]
        [Get("resultat_equipe_poule")]
        public Task<PagedResult<ResultatEquipePouleDto>> GetResultatEquipePoule([FromQuery] BrowseResultatEquipePoules query);

        [AllowAnyStatusCode]
        [Get("resultat_equipe_classement")]
        public Task<PagedResult<ResultatEquipeClassementDto>> GetResultatEquipeClassement([FromQuery] BrowseResultatEquipeClassements query);

        [AllowAnyStatusCode]
        [Get("rencontre")]
        public Task<PagedResult<RencontreDto>> GetRencontres([FromQuery] BrowseRencontres query);
        [AllowAnyStatusCode]
        [Get("equipes")]
        public Task<PagedResult<EquipeDto>> GetEquipes([FromQuery] BrowseEquipes query);

        [AllowAnyStatusCode]
        [Get("resultat_individuel_poule")]
        public Task<PagedResult<ResultatIndividuelPouleDto>> GetResultatIndividuelPoule([FromQuery] BrowseResultatIndividuelPoules query);
        [AllowAnyStatusCode]
        [Get("resultat_individuel_classement")]
        public Task<PagedResult<ResultatIndividuelClassementDto>> GetResultatIndividuelClassement([FromQuery] BrowseResultatIndividuelClassement query);
        [AllowAnyStatusCode]
        [Get("resultat_individuel_partie")]
        public Task<PagedResult<ResultatIndividuelPartieDto>> GetResultatIndividuelPartie([FromQuery] BrowseResultatIndividuelPartie query);

        [AllowAnyStatusCode]
        [Get("resultat_classement")]
        public Task<PagedResult<ClassementJoueurDto>> GetResultatClassement([FromQuery] BrowseClassementJoueurs query);
        [AllowAnyStatusCode]
        [Get("joueurs")]
        public Task<PagedResult<JoueurDto>> GetJoueurs([FromQuery] BrowseJoueur query);
        [AllowAnyStatusCode]
        [Get("joueur/{licence}")]
        public Task<JoueurDetailDto> GetJoueurDetail([Path] string licence);
        [AllowAnyStatusCode]
        [Get("licence/{licence}")]
        public Task<LicenceDto> GetLicence([Path] string licence);
        [AllowAnyStatusCode]
        [Get("licences")]
        public Task<PagedResult<LicenceDto>> GetLicences([FromQuery] BrowseLicences query);
        [AllowAnyStatusCode]
        [Get("parties")]
        public Task<PagedResult<PartieDto>> GetParties([FromQuery] BrowseParties query);
        [AllowAnyStatusCode]
        [Get("actus")]
        public Task<PagedResult<ActuFfttDto>> GetActualites([FromQuery] BrowseActuFftts query);
        [AllowAnyStatusCode]
        [Get("histo_clas")]
        public Task<PagedResult<HistoriqueClassementDto>> GetHistoriqueClassements([FromQuery] BrowseHistoriqueClassements query);
    }
}

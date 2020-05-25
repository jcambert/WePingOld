using MicroS_Common;
using MicroS_Common.Controllers;
using MicroS_Common.Dispatchers;
using MicroS_Common.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using WePing.domain.ActuFftts.Dto;
using WePing.domain.ActuFftts.Queries;
using WePing.domain.ClassementJoueurs.Dto;
using WePing.domain.ClassementJoueurs.Queries;
using WePing.domain.ClubDetails.Dto;
using WePing.domain.ClubDetails.Queries;
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
using WePing.domain.JoueurDetails.Queries;
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
using WePing.domain.Services;

namespace WePing.Service.Spid.Controller
{

    /// <summary>
    /// Spid Home Service
    /// </summary>
    [Route("")]
    public class HomeController : BaseController
    {
        public HomeController(
            IDispatcher dispatcher,
            IConfiguration config,
            IOptions<AppOptions> appOptions) : 
            base(dispatcher,config,appOptions)
        {
            
        }


        /// <summary>
        /// Get list of Clubs
        /// you can pass as parameter
        /// dep for Department
        /// id for club id
        /// numero for club Number
        /// ville fot club's town
        /// </summary>
        /// <param name="query"></param>
        /// <returns>a paginated club list</returns>
        [HttpGet("clubs")]
        public async Task<ActionResult<PagedResult<ClubDto>>> GetClubs([FromQuery] BrowseClubs query)
        {
            return Collection(await QueryAsync(query));
        }

        /// <summary>
        /// get club's details
        /// you can pass as parameter
        /// club for club id
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("club/{club}")]
        public async Task<ActionResult<ClubDetailDto>> GetClubDetail([FromRoute] GetClubDetail query)
        {
            return Single(await QueryAsync(query));
        }

        [HttpGet("organismes")]
        public async Task<ActionResult<PagedResult<OrganismeDto>>> GetOrganismes([FromQuery] BrowseOrganismes query)
        {
            
            return Collection(await QueryAsync(query));
        }

        [HttpGet("epreuves")]
        public async Task<ActionResult<PagedResult<EpreuveDto>>> GetEpreuves([FromQuery] BrowseEpreuves query)
        {
            return Collection(await QueryAsync(query));
        }

        [HttpGet("divisions")]
        public async Task<ActionResult<PagedResult<DivisionDto>>> GetDivisions([FromQuery] BrowseDivisions query)
        {
            return Collection(await QueryAsync(query));
        }
        [HttpGet("resultat_equipe_rencontre")]
        public async Task<ActionResult<PagedResult<ResultatEquipeRencontreDto>>> GetResultatEquipeRencontre([FromQuery] BrowseResultatEquipeRencontres query)
        {
            return Collection(await QueryAsync(query));
        }
        [HttpGet("resultat_equipe_poule")]
        public async Task<ActionResult<PagedResult<ResultatEquipePouleDto>>> GetResultatEquipePoule([FromQuery] BrowseResultatEquipePoules query)
        {
            return Collection(await QueryAsync(query));
        }

        [HttpGet("resultat_equipe_classement")]
        public async Task<ActionResult<PagedResult<ResultatEquipeClassementDto>>> GetResultatEquipeClassement([FromQuery] BrowseResultatEquipeClassements query)
        {
            return Collection(await QueryAsync(query));
        }

       

        [HttpGet("equipes")]
        public async Task<ActionResult<PagedResult<EquipeDto>>> GetEquipes([FromQuery] BrowseEquipes query)
        {
            return Collection(await QueryAsync(query));
        }

        [HttpGet("resultat_individuel_poule")]
        public async Task<ActionResult<PagedResult<ResultatIndividuelPouleDto>>> GetResultatIndividuelPoule([FromQuery] BrowseResultatIndividuelPoules query)
        {
            return Collection(await QueryAsync(query));
        }

        [HttpGet("resultat_individuel_classement")]
        public async Task<ActionResult<PagedResult<ResultatIndividuelClassementDto>>> GetResultatIndividuelClassement([FromQuery] BrowseResultatIndividuelClassement query)
        {
            return Collection(await QueryAsync(query));
        }

        [HttpGet("resultat_individuel_partie")]
        public async Task<ActionResult<PagedResult<ResultatIndividuelPartieDto>>> GetResultatIndividuelPartie([FromQuery] BrowseResultatIndividuelPartie query)
        {
            return Collection(await QueryAsync(query));
        }

        [HttpGet("resultat_classement")]
        public async Task<ActionResult<PagedResult<ClassementJoueurDto>>> GetResultatClassement([FromQuery] BrowseClassementJoueurs query)
        {
            return Collection(await QueryAsync(query));
        }

        [HttpGet("joueurs")]
        public async Task<ActionResult<PagedResult<JoueurDto>>> GetJoueurs([FromQuery] BrowseJoueur query)
        {
            return Collection(await QueryAsync(query));
        }

        [HttpGet("rencontre")]
        public async Task<ActionResult<RencontreDto>> GetRencontre([FromQuery] GetRencontre query)
        {
            //return Ok();
            return Single(await QueryAsync(query));
        }
        [HttpGet("joueur/{licence}")]
        public async Task<ActionResult<JoueurDetailDto>> GetJoueurDetail([FromRoute] GetJoueurDetail query)
        {
            return Single(await QueryAsync(query));
        }

        [HttpGet("licence/{licence}")]
        public async Task<ActionResult<LicenceDto>> GetLicence([FromRoute] GetLicence query)
        {
            return Single(await QueryAsync(query));
        }

        [HttpGet("licences")]
        public async Task<ActionResult<PagedResult<LicenceDto>>> GetLicences([FromQuery] BrowseLicences query)
        {
            return Collection(await QueryAsync(query));
        }

        [HttpGet("parties")]
        public async Task<ActionResult<PagedResult<PartieDto>>> GetParties([FromQuery] BrowseParties query)
        {
            return Collection(await QueryAsync(query));
        }

        [HttpGet("actus")]
        public async Task<ActionResult<PagedResult<ActuFfttDto>>> GetActualites([FromQuery] BrowseActuFftts query)
        {
            return Collection(await QueryAsync(query));
        }
        [HttpGet("histo_clas")]
        public async Task<ActionResult<PagedResult<HistoriqueClassementDto>>> GetHistoriqueClassements([FromQuery] BrowseHistoriqueClassements query)
        {
            return Collection(await QueryAsync(query));
        }

    }

}
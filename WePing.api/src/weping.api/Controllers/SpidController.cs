using MicroS_Common;
using MicroS_Common.Controllers;
using MicroS_Common.Dispatchers;
using MicroS_Common.RabbitMq;
using MicroS_Common.Redis;
using MicroS_Common.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenTracing;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using weping.api.Framework;
using weping.api.Services;
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

namespace weping.api.Controllers
{
    [AdminAuth]
    public class SpidController : ApiBaseController
    {
        private readonly ISpidService _spid;
        private readonly ILogger<SpidController> _logger;

        public SpidController(
            IBusPublisher busPublisher, 
            ITracer tracer,
            IDispatcher dispatcher, 
            IConfiguration configuration, 
            IOptions<AppOptions> appOptions,
            ISpidService service, 
            ILogger<SpidController> logger) : 
            base(busPublisher, tracer,dispatcher,configuration,appOptions)
        {
            _spid = service;
            _logger = logger;
            _logger.LogInformation("SpidController Created");
        }

        [Cached()]
        [HttpGet("clubs")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResult<ClubDto>>> GetClubs([FromQuery] BrowseClubs query)
            => Collection(await _spid.GetClubs(query));

        [Cached()]
        [HttpGet("club/{club}")]
        [AllowAnonymous]
        public async Task<ActionResult<ClubDetailDto>> GetClubDetail([FromRoute] GetClubDetail query)
        => Single(await _spid.GetClubDetail(query.Club));

        [Cached()]
        [HttpGet("organismes")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResult<OrganismeDto>>> GetOrganismes([FromQuery] BrowseOrganismes query)
        {
            if (string.IsNullOrEmpty(query.Type) || query.Type == "A")
            {
                List<string> ogs = new List<string>() { "F", "Z", "L", "D" };
                PagedResult<OrganismeDto> result = PagedResult<OrganismeDto>.Empty;
                IEnumerable<Task<PagedResult<OrganismeDto>>> tasks = from req in ogs select _spid.GetOrganismes(new BrowseOrganismes() { Type = req ,Results=System.Int32.MaxValue});

              
                var re=await Task.WhenAll(tasks.ToArray());
                re.ToList().ForEach(r => result.AddRange(r));
                return Collection(result);
            }

            return Collection(await _spid.GetOrganismes(query));

        }

        [Cached()]
        [HttpGet("epreuves")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResult<EpreuveDto>>> GetEpreuves([FromQuery] BrowseEpreuves query)
            => Collection(await _spid.GetEpreuves(query));

        [HttpGet("divisions")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResult<DivisionDto>>> GetDivisions([FromQuery] BrowseDivisions query)
            => Collection(await _spid.GetDivisions(query));

        [Cached()]
        [HttpGet("resultat_equipe_rencontre")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResult<ResultatEquipeRencontreDto>>> GetResultatEquipeRencontre([FromQuery] BrowseResultatEquipeRencontres query)
            => Collection(await _spid.GetResultatEquipeRencontre(query));

        [Cached()]
        [HttpGet("resultat_equipe_poule")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResult<ResultatEquipePouleDto>>> GetResultatEquipePoule([FromQuery] BrowseResultatEquipePoules query)
            => Collection(await _spid.GetResultatEquipePoule(query));

        [Cached()]
        [HttpGet("resultat_equipe_classement")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResult<ResultatEquipeClassementDto>>> GetResultatEquipeClassement([FromQuery] BrowseResultatEquipeClassements query)
            => Collection(await _spid.GetResultatEquipeClassement(query));

        

        [Cached()]
        [HttpGet("equipes")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResult<EquipeDto>>> GetEquipes([FromQuery] BrowseEquipes query)
           => Collection(await _spid.GetEquipes(query));

        [Cached()]
        [HttpGet("resultat_individuel_poule")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResult<ResultatIndividuelPouleDto>>> GetResultatIndividuelPoule([FromQuery] BrowseResultatIndividuelPoules query)
            => Collection(await _spid.GetResultatIndividuelPoule(query));

        [Cached()]
        [HttpGet("resultat_individuel_classement")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResult<ResultatIndividuelClassementDto>>> GetResultatIndividuelClassement([FromQuery] BrowseResultatIndividuelClassement query)
            => Collection(await _spid.GetResultatIndividuelClassement(query));

        [Cached()]
        [HttpGet("resultat_individuel_partie")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResult<ResultatIndividuelPartieDto>>> GetResultatIndividuelPartie([FromQuery] BrowseResultatIndividuelPartie query)
            => Collection(await _spid.GetResultatIndividuelPartie(query));

        [Cached()]
        [HttpGet("resultat_classement")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResult<ClassementJoueurDto>>> GetResultatClassement([FromQuery] BrowseClassementJoueurs query)
            => Collection(await _spid.GetResultatClassement(query));

        [Cached()]
        [HttpGet("joueurs")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResult<JoueurDto>>> GetJoueurs([FromQuery] BrowseJoueur query)
            => Collection(await _spid.GetJoueurs(query));

        [Cached()]
        [HttpGet("joueur/{licence}")]
        [AllowAnonymous]
        public async Task<ActionResult<JoueurDetailDto>> GetJoueurDetail([FromRoute] GetJoueurDetail query)
            => Single(await _spid.GetJoueurDetail(query.Licence));

        [Cached()]
        [HttpGet("licence/{licence}")]
        [AllowAnonymous]
        public async Task<ActionResult<LicenceDto>> GetLicence([FromRoute] GetLicence query)
            => Single(await _spid.GetLicence(query.Licence));
        [Cached()]
        [HttpGet("rencontre")]
        [AllowAnonymous]
        public async Task<ActionResult<RencontreDto>> GetRencontre([FromQuery] GetRencontre query)
            => Single(await _spid.GetRencontre(query));

        [Cached()]
        [HttpGet("licences")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResult<LicenceDto>>> GetLicences([FromQuery] BrowseLicences query)
            => Collection(await _spid.GetLicences(query));

        [Cached()]
        [HttpGet("parties")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResult<PartieDto>>> GetParties([FromQuery] BrowseParties query)
            => Collection(await _spid.GetParties(query));

        [HttpGet("actus")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResult<ActuFfttDto>>> GetActualites([FromQuery] BrowseActuFftts query)
            => Collection(await _spid.GetActualites(query));

        [HttpGet("histo_clas")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResult<HistoriqueClassementDto>>> GetHistoriqueClassements([FromQuery] BrowseHistoriqueClassements query)
            => Collection(await _spid.GetHistoriqueClassements(query));
    }
}
using MicroS_Common.RabbitMq;
using MicroS_Common.Redis;
using MicroS_Common.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OpenTracing;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using weping.api.Framework;
using weping.api.Services;
using WePing.domain.ActuFftts.Queries;
using WePing.domain.ClassementJoueurs.Queries;
using WePing.domain.ClubDetails.Queries;
using WePing.domain.Clubs.Queries;
using WePing.domain.Divisions.Queries;
using WePing.domain.Epreuves.Queries;
using WePing.domain.Equipes.Queries;
using WePing.domain.HistoriqueClassements.Queries;
using WePing.domain.JoueurDetails.Queries;
using WePing.domain.Joueurs.Queries;
using WePing.domain.Licences.Queries;
using WePing.domain.Organismes.Dto;
using WePing.domain.Organismes.Queries;
using WePing.domain.Parties.Queries;
using WePing.domain.Rencontres.Queries;
using WePing.domain.ResultatEquipeRencontres.Queries;
using WePing.domain.ResultatIndividuels.Queries;
namespace weping.api.Controllers
{
    [AdminAuth]
    public class SpidController : BaseController
    {
        private readonly ISpidService _spid;
        private readonly ILogger<SpidController> _logger;

        public SpidController(IBusPublisher busPublisher, ITracer tracer,
            ISpidService service, ILogger<SpidController> logger) : base(busPublisher, tracer)
        {
            _spid = service;
            _logger = logger;
            _logger.LogInformation("SpidController Created");
        }

        [Cached()]
        [HttpGet("clubs")]
        [AllowAnonymous]
        public async Task<IActionResult> GetClubs([FromQuery] BrowseClubs query)
            => Collection(await _spid.GetClubs(query));

        [Cached()]
        [HttpGet("club/{club}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetClubDetail([FromRoute] GetClubDetail query)
        => Single(await _spid.GetClubDetail(query.Club));

        [Cached()]
        [HttpGet("organismes")]
        [AllowAnonymous]
        public async Task<IActionResult> GetOrganismes([FromQuery] BrowseOrganismes query)
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
        public async Task<IActionResult> GetEpreuves([FromQuery] BrowseEpreuves query)
            => Collection(await _spid.GetEpreuves(query));

        [HttpGet("divisions")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDivisions([FromQuery] BrowseDivisions query)
            => Collection(await _spid.GetDivisions(query));

        [Cached()]
        [HttpGet("resultat_equipe_rencontre")]
        [AllowAnonymous]
        public async Task<IActionResult> GetResultatEquipeRencontre([FromQuery] BrowseResultatEquipeRencontres query)
            => Collection(await _spid.GetResultatEquipeRencontre(query));

        [Cached()]
        [HttpGet("resultat_equipe_poule")]
        public async Task<IActionResult> GetResultatEquipePoule([FromQuery] BrowseResultatEquipePoules query)
            => Collection(await _spid.GetResultatEquipePoule(query));

        [Cached()]
        [HttpGet("resultat_equipe_classement")]
        [AllowAnonymous]
        public async Task<IActionResult> GetResultatEquipeClassement([FromQuery] BrowseResultatEquipeClassements query)
            => Collection(await _spid.GetResultatEquipeClassement(query));

        

        [Cached()]
        [HttpGet("equipes")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEquipes([FromQuery] BrowseEquipes query)
           => Collection(await _spid.GetEquipes(query));

        [Cached()]
        [HttpGet("resultat_individuel_poule")]
        [AllowAnonymous]
        public async Task<IActionResult> GetResultatIndividuelPoule([FromQuery] BrowseResultatIndividuelPoules query)
            => Collection(await _spid.GetResultatIndividuelPoule(query));

        [Cached()]
        [HttpGet("resultat_individuel_classement")]
        [AllowAnonymous]
        public async Task<IActionResult> GetResultatIndividuelClassement([FromQuery] BrowseResultatIndividuelClassement query)
            => Collection(await _spid.GetResultatIndividuelClassement(query));

        [Cached()]
        [HttpGet("resultat_individuel_partie")]
        [AllowAnonymous]
        public async Task<IActionResult> GetResultatIndividuelPartie([FromQuery] BrowseResultatIndividuelPartie query)
            => Collection(await _spid.GetResultatIndividuelPartie(query));

        [Cached()]
        [HttpGet("resultat_classement")]
        [AllowAnonymous]
        public async Task<IActionResult> GetResultatClassement([FromQuery] BrowseClassementJoueurs query)
            => Collection(await _spid.GetResultatClassement(query));

        [Cached()]
        [HttpGet("joueurs")]
        [AllowAnonymous]
        public async Task<IActionResult> GetJoueurs([FromQuery] BrowseJoueur query)
            => Collection(await _spid.GetJoueurs(query));

        [Cached()]
        [HttpGet("joueur/{licence}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetJoueurDetail([FromRoute] GetJoueurDetail query)
            => Single(await _spid.GetJoueurDetail(query.Licence));

        [Cached()]
        [HttpGet("licence/{licence}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLicence([FromRoute] GetLicence query)
            => Single(await _spid.GetLicence(query.Licence));
        [Cached()]
        [HttpGet("rencontre")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRencontre([FromQuery] GetRencontre query)
            => Single(await _spid.GetRencontre(query));

        [Cached()]
        [HttpGet("licences")]
        [AllowAnonymous]
        // [ExcelFilter]
        public async Task<IActionResult> GetLicences([FromQuery] BrowseLicences query)
            => Collection(await _spid.GetLicences(query));

        [Cached()]
        [HttpGet("parties")]
        [AllowAnonymous]
        public async Task<IActionResult> GetParties([FromQuery] BrowseParties query)
            => Collection(await _spid.GetParties(query));

        [HttpGet("actus")]
        [AllowAnonymous]
        public async Task<IActionResult> GetActualites([FromQuery] BrowseActuFftts query)
            => Collection(await _spid.GetActualites(query));

        [HttpGet("histo_clas")]
        [AllowAnonymous]
        public async Task<IActionResult> GetHistoriqueClassements([FromQuery] BrowseHistoriqueClassements query)
            => Collection(await _spid.GetHistoriqueClassements(query));
    }
}
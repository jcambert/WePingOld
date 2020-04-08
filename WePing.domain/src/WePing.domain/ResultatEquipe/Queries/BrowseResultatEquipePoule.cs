using MicroS_Common.Types;
using WePing.domain.ResultatEquipeRencontres.Dto;

namespace WePing.domain.ResultatEquipeRencontres.Queries
{
    public class BrowseResultatEquipePoules : PagedQueryBase, IQuery<PagedResult<ResultatEquipePouleDto>>
    {
        public string Action { get; } = "poule";

        public string Auto { get; } = "1";
        [UpperCase]
        public string D1 { get; set; }

        public string Cx_poule { get; set; }
    }
}

using MicroS_Common.Types;
using WePing.domain.ResultatEquipeRencontres.Dto;

namespace WePing.domain.ResultatEquipeRencontres.Queries
{
    public class BrowseResultatEquipeClassements : PagedQueryBase, IQuery<PagedResult<ResultatEquipeClassementDto>>
    {
        public string Action { get; } = "classement";

        public string Auto { get; } = "1";
        [UpperCase]
        public string D1 { get; set; }

        public string Cx_poule { get; set; }
    }
}

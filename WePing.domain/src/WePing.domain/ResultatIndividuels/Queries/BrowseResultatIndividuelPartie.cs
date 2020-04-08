using MicroS_Common.Types;
using WePing.domain.ResultatIndividuels.Dto;

namespace WePing.domain.ResultatIndividuels.Queries
{
    public class BrowseResultatIndividuelPartie : PagedQueryBase, IQuery<PagedResult<ResultatIndividuelPartieDto>>
    {
        public string Action { get; } = "partie";
        public string Epr { get; set; }
        public string Res_Division { get; set; }
        public string Cx_Tableau { get; set; }
    }
}

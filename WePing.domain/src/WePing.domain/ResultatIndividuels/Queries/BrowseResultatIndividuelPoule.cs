using MicroS_Common.Types;
using WePing.domain.ResultatIndividuels.Dto;

namespace WePing.domain.ResultatIndividuels.Queries
{
    public class BrowseResultatIndividuelPoules : PagedQueryBase, IQuery<PagedResult<ResultatIndividuelPouleDto>>
    {
        public string Action { get; } = "classement";
        public string Epr { get; set; }
        public string Res_Division { get; set; }
        public string Cx_Tableau { get; set; }
    }
}

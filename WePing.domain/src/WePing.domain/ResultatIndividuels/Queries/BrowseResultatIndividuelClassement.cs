using MicroS_Common.Types;
using WePing.domain.ResultatIndividuels.Dto;

namespace WePing.domain.ResultatIndividuels.Queries
{
    public class BrowseResultatIndividuelClassement : PagedQueryBase, IQuery<PagedResult<ResultatIndividuelClassementDto>>
    {

        public string Action { get; } = "classement";
        public string Epr { get; set; }
        public string Res_Division { get; set; }
        public string Cx_Tableau { get; set; }
    }
}

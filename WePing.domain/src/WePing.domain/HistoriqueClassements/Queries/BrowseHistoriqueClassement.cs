using MicroS_Common.Types;
using WePing.domain.HistoriqueClassements.Dto;

namespace WePing.domain.HistoriqueClassements.Queries
{
    public class BrowseHistoriqueClassements : PagedQueryBase, IQuery<PagedResult<HistoriqueClassementDto>>
    {
        public string NumLic { get; set; }
    }
}

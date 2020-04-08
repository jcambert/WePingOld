using MicroS_Common.Types;
using WePing.domain.JoueurDetails.Dto;

namespace WePing.domain.JoueurDetails.Queries
{
    public class BrowseJoueurDetails : PagedQueryBase, IQuery<PagedResult<JoueurDetailDto>>
    {
    }
}

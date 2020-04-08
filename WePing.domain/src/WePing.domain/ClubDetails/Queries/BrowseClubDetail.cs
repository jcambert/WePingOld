using MicroS_Common.Types;
using WePing.domain.ClubDetails.Dto;

namespace WePing.domain.ClubDetails.Queries
{
    public class BrowseClubDetails : PagedQueryBase, IQuery<PagedResult<ClubDetailDto>>
    {
    }
}

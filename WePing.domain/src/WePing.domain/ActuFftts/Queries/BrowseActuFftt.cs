using MicroS_Common.Types;
using WePing.domain.ActuFftts.Dto;

namespace WePing.domain.ActuFftts.Queries
{
    public class BrowseActuFftts : PagedQueryBase, IQuery<PagedResult<ActuFfttDto>>
    {
    }
}

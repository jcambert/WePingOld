using MicroS_Common.Types;
using WePing.domain.Parties.Dto;

namespace WePing.domain.Parties.Queries
{
    public class BrowseParties : PagedQueryBase, IQuery<PagedResult<PartieDto>>
    {
        public string Licence { get; set; }

        public string NumLic => Licence;
    }
}

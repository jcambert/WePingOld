using MicroS_Common.Types;
using WePing.domain.Organismes.Dto;

namespace WePing.domain.Organismes.Queries
{
    public class BrowseOrganismes : PagedQueryBase, IQuery<PagedResult<OrganismeDto>>
    {
        public string Type { get; set; }
    }
}

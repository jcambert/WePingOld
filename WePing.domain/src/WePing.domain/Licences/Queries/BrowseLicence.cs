using MicroS_Common.Types;
using WePing.domain.Licences.Dto;

namespace WePing.domain.Licences.Queries
{
    public class BrowseLicences : PagedQueryBase, IQuery<PagedResult<LicenceDto>>
    {
        public string Club { get; set; }
    }
}

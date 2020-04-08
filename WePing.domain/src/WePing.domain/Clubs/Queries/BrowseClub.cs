using MicroS_Common.Types;
using System.ComponentModel;
using WePing.domain.Clubs.Dto;
namespace WePing.domain.Clubs.Queries
{
    public class BrowseClubs : PagedQueryBase, IQuery<PagedResult<ClubDto>>
    {
        [Default]
        [Description("Département")]
        public string Dep { get; set; }

        public string Code { get; set; }

        public string Ville { get; set; }

        public string Numero { get; set; }
    }
}

using MicroS_Common.Types;
using WePing.domain.ClassementJoueurs.Dto;

namespace WePing.domain.ClassementJoueurs.Queries
{
    public class BrowseClassementJoueurs : PagedQueryBase, IQuery<PagedResult<ClassementJoueurDto>>
    {
        public string Res_Division { get; set; }
    }
}

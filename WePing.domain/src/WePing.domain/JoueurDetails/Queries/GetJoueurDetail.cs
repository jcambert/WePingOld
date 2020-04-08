using MicroS_Common.Types;
using WePing.domain.JoueurDetails.Dto;

namespace WePing.domain.JoueurDetails.Queries
{
    public class GetJoueurDetail : IQuery<JoueurDetailDto>
    {
        #region public properties
        public string Licence { get; set; }
        #endregion

    }
}

using MicroS_Common.Types;
using WePing.domain.Licences.Dto;

namespace WePing.domain.Licences.Queries
{
    public class GetLicence : IQuery<LicenceDto>
    {
        #region public properties
        public string Licence { get; set; }
        #endregion

    }
}

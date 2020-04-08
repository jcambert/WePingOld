using AutoMapper;
using System;
using System.Threading.Tasks;
using WePing.domain.Licences.Domain;
using WePing.domain.Licences.Dto;
using WePing.domain.Licences.Queries;
using WePing.Service.Spid.Services;

namespace WePing.Service.Spid.Handlers
{
    public class GetLicenceHandler : BaseGet<Licence, GetLicence, LicenceDto>
    {
        public GetLicenceHandler(IMapper mapper, ISpidRequest request) : base(mapper, request)
        {
        }

        public override Func<GetLicence, Task<Licence>> Execute => Spid.GetLicence;
    }

}

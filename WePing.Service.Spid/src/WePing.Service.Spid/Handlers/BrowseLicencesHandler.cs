using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WePing.domain.Licences.Domain;
using WePing.domain.Licences.Dto;
using WePing.domain.Licences.Queries;
using WePing.Service.Spid.Services;

namespace WePing.Service.Spid.Handlers
{
    public class BrowseLicencesHandler : BaseBrowse<Licence, BrowseLicences, LicenceDto>
    {
        public BrowseLicencesHandler(IMapper mapper, ISpidRequest request) : base(mapper, request)
        {
        }

        public override Func<BrowseLicences, Task<List<Licence>>> Execute => Spid.GetLicences;
    }

}

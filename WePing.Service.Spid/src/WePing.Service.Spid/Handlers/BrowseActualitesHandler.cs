using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WePing.domain.ActuFftts.Domain;
using WePing.domain.ActuFftts.Dto;
using WePing.domain.ActuFftts.Queries;
using WePing.Service.Spid.Services;

namespace WePing.Service.Spid.Handlers
{
    public class BrowseActualitesHandler : BaseBrowse<ActuFftt, BrowseActuFftts, ActuFfttDto>
    {
        public BrowseActualitesHandler(IMapper mapper, ISpidRequest request) : base(mapper, request)
        {
        }

        public override Func<BrowseActuFftts, Task<List<ActuFftt>>> Execute => Spid.GetActus;
    }

}

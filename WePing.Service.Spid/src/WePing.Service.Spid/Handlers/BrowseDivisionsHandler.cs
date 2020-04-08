using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WePing.domain.Divisions.Domain;
using WePing.domain.Divisions.Dto;
using WePing.domain.Divisions.Queries;
using WePing.Service.Spid.Services;

namespace WePing.Service.Spid.Handlers
{
    public class BrowseDivisionsHandler : BaseBrowse<Division, BrowseDivisions, DivisionDto>
    {
        public BrowseDivisionsHandler(IMapper mapper, ISpidRequest request) : base(mapper, request)
        {
        }

        public override Func<BrowseDivisions, Task<List<Division>>> Execute => Spid.GetDivisions;
    }

}

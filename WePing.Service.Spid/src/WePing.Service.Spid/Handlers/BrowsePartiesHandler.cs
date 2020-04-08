using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WePing.domain.Parties.Domain;
using WePing.domain.Parties.Dto;
using WePing.domain.Parties.Queries;
using WePing.Service.Spid.Services;

namespace WePing.Service.Spid.Handlers
{
    public class BrowsePartiesHandler : BaseBrowse<Partie, BrowseParties, PartieDto>
    {
        public BrowsePartiesHandler(IMapper mapper, ISpidRequest request) : base(mapper, request)
        {
        }

        public override Func<BrowseParties, Task<List<Partie>>> Execute => Spid.GetParties;
    }

}

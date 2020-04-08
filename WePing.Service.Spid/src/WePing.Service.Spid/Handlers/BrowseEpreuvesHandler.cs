using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WePing.domain.Epreuves.Domain;
using WePing.domain.Epreuves.Dto;
using WePing.domain.Epreuves.Queries;
using WePing.Service.Spid.Services;

namespace WePing.Service.Spid.Handlers
{
    public class BrowseEpreuvesHandler : BaseBrowse<Epreuve, BrowseEpreuves, EpreuveDto>
    {
        public BrowseEpreuvesHandler(IMapper mapper, ISpidRequest request) : base(mapper, request)
        {
        }

        public override Func<BrowseEpreuves, Task<List<Epreuve>>> Execute => Spid.GetEpreuves;
    }

}

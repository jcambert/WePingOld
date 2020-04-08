using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WePing.domain.ResultatIndividuels.Domain;
using WePing.domain.ResultatIndividuels.Dto;
using WePing.domain.ResultatIndividuels.Queries;
using WePing.Service.Spid.Services;

namespace WePing.Service.Spid.Handlers
{
    public class BrowseResultatIndividuelPoulesHandler : BaseBrowse<ResultatIndividuelPoule, BrowseResultatIndividuelPoules, ResultatIndividuelPouleDto>
    {
        public BrowseResultatIndividuelPoulesHandler(IMapper mapper, ISpidRequest request) : base(mapper, request)
        {
        }

        public override Func<BrowseResultatIndividuelPoules, Task<List<ResultatIndividuelPoule>>> Execute => Spid.GetResultatIndividuelPoules;
    }

}

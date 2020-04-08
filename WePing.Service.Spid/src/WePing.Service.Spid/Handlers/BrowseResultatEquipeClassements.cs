using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WePing.domain.ResultatEquipeRencontres.Domain;
using WePing.domain.ResultatEquipeRencontres.Dto;
using WePing.domain.ResultatEquipeRencontres.Queries;
using WePing.Service.Spid.Services;

namespace WePing.Service.Spid.Handlers
{
    public class BrowseResultatEquipeClassementsHandler : BaseBrowse<ResultatEquipeClassement, BrowseResultatEquipeClassements, ResultatEquipeClassementDto>
    {
        public BrowseResultatEquipeClassementsHandler(IMapper mapper, ISpidRequest request) : base(mapper, request)
        {
        }


        public override Func<BrowseResultatEquipeClassements, Task<List<ResultatEquipeClassement>>> Execute => Spid.GetResultatEquipeClassements;
    }

}

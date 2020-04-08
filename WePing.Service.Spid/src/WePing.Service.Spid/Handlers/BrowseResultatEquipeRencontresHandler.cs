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
    public class BrowseResultatEquipeRencontresHandler : BaseBrowse<ResultatEquipeRencontre, BrowseResultatEquipeRencontres, ResultatEquipeRencontreDto>
    {
        public BrowseResultatEquipeRencontresHandler(IMapper mapper, ISpidRequest request) : base(mapper, request)
        {
        }

        public override Func<BrowseResultatEquipeRencontres, Task<List<ResultatEquipeRencontre>>> Execute => Spid.GetResultatEquipeRencontres;
    }

}

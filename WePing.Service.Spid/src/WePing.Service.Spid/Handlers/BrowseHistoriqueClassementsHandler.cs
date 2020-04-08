using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WePing.domain.HistoriqueClassements.Domain;
using WePing.domain.HistoriqueClassements.Dto;
using WePing.domain.HistoriqueClassements.Queries;
using WePing.Service.Spid.Services;

namespace WePing.Service.Spid.Handlers
{
    public class BrowseHistoriqueClassementsHandler : BaseBrowse<HistoriqueClassement, BrowseHistoriqueClassements, HistoriqueClassementDto>
    {
        public BrowseHistoriqueClassementsHandler(IMapper mapper, ISpidRequest request) : base(mapper, request)
        {
        }

        public override Func<BrowseHistoriqueClassements, Task<List<HistoriqueClassement>>> Execute => Spid.GetHistoriqueClassements;
    }

}

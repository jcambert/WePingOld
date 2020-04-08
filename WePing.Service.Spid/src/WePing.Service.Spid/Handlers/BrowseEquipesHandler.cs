using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WePing.domain.Equipes.Domain;
using WePing.domain.Equipes.Dto;
using WePing.domain.Equipes.Queries;
using WePing.Service.Spid.Services;

namespace WePing.Service.Spid.Handlers
{
    public class BrowseEquipesHandler : BaseBrowse<Equipe, BrowseEquipes, EquipeDto>
    {
        public BrowseEquipesHandler(IMapper mapper, ISpidRequest request) : base(mapper, request)
        {
        }

        public override Func<BrowseEquipes, Task<List<Equipe>>> Execute => Spid.GetEquipes;
    }

}

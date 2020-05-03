using AutoMapper;
using System;
using System.Threading.Tasks;
using WePing.domain.Rencontres.Domain;
using WePing.domain.Rencontres.Dto;
using WePing.domain.Rencontres.Queries;
using WePing.Service.Spid.Services;

namespace WePing.Service.Spid.Handlers
{
    public class GetRencontreHandler : BaseGet<Rencontre, GetRencontre, RencontreDto>
    {
        public GetRencontreHandler(IMapper mapper, ISpidRequest request) : base(mapper, request)
        {
        }

        public override Func<GetRencontre, Task<Rencontre>> Execute => Spid.GetRencontre;
    
    }
}

using AutoMapper;
using System;
using System.Threading.Tasks;
using WePing.domain.JoueurDetails.Domain;
using WePing.domain.JoueurDetails.Dto;
using WePing.domain.JoueurDetails.Queries;
using WePing.Service.Spid.Services;

namespace WePing.Service.Spid.Handlers
{
    public class GetJoueurDetailsHandler : BaseGet<JoueurDetail, GetJoueurDetail, JoueurDetailDto>
    {
        public GetJoueurDetailsHandler(IMapper mapper, ISpidRequest request) : base(mapper, request)
        {
        }

        public override Func<GetJoueurDetail, Task<JoueurDetail>> Execute => Spid.GetJoueurDetails;
    }

}

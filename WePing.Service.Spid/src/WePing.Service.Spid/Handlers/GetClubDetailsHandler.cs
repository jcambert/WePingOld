using AutoMapper;
using System;
using System.Threading.Tasks;
using WePing.domain.ClubDetails.Domain;
using WePing.domain.ClubDetails.Dto;
using WePing.domain.ClubDetails.Queries;
using WePing.Service.Spid.Services;

namespace WePing.Service.Spid.Handlers
{
    public class GetClubDetailsHandler : BaseGet<ClubDetail, GetClubDetail, ClubDetailDto>
    {
        public GetClubDetailsHandler(IMapper mapper, ISpidRequest request) : base(mapper, request)
        {
        }

        public override Func<GetClubDetail, Task<ClubDetail>> Execute => Spid.GetClubDetails;
    }

}

using MicroS_Common.Types;
using WePing.domain.ClubDetails.Dto;

namespace WePing.domain.ClubDetails.Queries
{
    public class GetClubDetail : IQuery<ClubDetailDto>
    {
        public string Club { get; set; }
    }
}

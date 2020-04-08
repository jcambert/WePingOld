using MicroS_Common.Types;
using WePing.domain.Clubs.Dto;

namespace WePing.domain.Clubs.Queries
{
    public class GetClub : IQuery<ClubDto>
    {
        public string Numero { get; set; }
    }
}

using AutoMapper;
using WePing.domain.Clubs.Domain;
using WePing.domain.Clubs.Dto;

namespace WePing.domain.Clubs.Mapping
{
    public class ClubProfile : Profile
    {
        public ClubProfile()
        {
            CreateMap<Club, ClubDto>().ConstructUsing(e => new ClubDto() { Id = e.Id, Numero = e.Numero, Nom = e.Nom, Validation = e.Validation });

        }
    }
}

using AutoMapper;
using WePing.domain.Rencontres.Domain;
using WePing.domain.Rencontres.Dto;

namespace WePing.domain.Rencontres.Mapping
{
    public class RencontreProfile : Profile
    {
        public RencontreProfile()
        {
            CreateMap<Rencontre, RencontreDto>().ConstructUsing(e => new RencontreDto() { Id = e.Id });
        }
    }
}

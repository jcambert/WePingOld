using AutoMapper;
using WePing.domain.Divisions.Domain;
using WePing.domain.Divisions.Dto;

namespace WePing.domain.Divisions.Mapping
{
    public class DivisionProfile : Profile
    {
        public DivisionProfile()
        {
            CreateMap<Division, DivisionDto>().ConstructUsing(e => new DivisionDto() { Id = e.Id, Libelle = e.Libelle });

        }
    }
}

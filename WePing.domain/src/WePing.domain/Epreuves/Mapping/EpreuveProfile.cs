using AutoMapper;
using WePing.domain.Epreuves.Domain;
using WePing.domain.Epreuves.Dto;

namespace WePing.domain.Epreuves.Mapping
{
    public class EpreuveProfile : Profile
    {
        public EpreuveProfile()
        {
            CreateMap<Epreuve, EpreuveDto>().ConstructUsing(e => new EpreuveDto() { Id = e.Id, Organisme = e.Organisme, Libelle = e.Libelle });

        }
    }
}

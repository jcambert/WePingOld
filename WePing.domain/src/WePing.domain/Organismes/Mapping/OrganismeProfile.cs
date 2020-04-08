using AutoMapper;
using WePing.domain.Organismes.Domain;
using WePing.domain.Organismes.Dto;

namespace WePing.domain.Organismes.Mapping
{
    public class OrganismeProfile : Profile
    {
        public OrganismeProfile()
        {
            CreateMap<Organisme, OrganismeDto>().ConstructUsing(e => new OrganismeDto() { Id = e.Id, Libelle = e.Libelle, Code = e.Code });

        }
    }
}

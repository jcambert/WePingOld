using AutoMapper;
using WePing.domain.Equipes.Domain;
using WePing.domain.Equipes.Dto;

namespace WePing.domain.Equipes.Mapping
{
    public class EquipeProfile : Profile
    {
        public EquipeProfile()
        {
            CreateMap<Equipe, EquipeDto>().ConstructUsing(e => new EquipeDto() { Nom = e.Nom, Division = e.Division, Id = e.Id, Epreuve = e.Epreuve, Lien = e.Lien });

        }
    }
}

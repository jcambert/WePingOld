using AutoMapper;
using WePing.domain.ResultatIndividuels.Domain;
using WePing.domain.ResultatIndividuels.Dto;

namespace WePing.domain.ResultatIndividuels.Mapping
{
    public class ResultatIndividuelProfile : Profile
    {
        public ResultatIndividuelProfile()
        {
            CreateMap<ResultatIndividuelPoule, ResultatIndividuelPouleDto>().ConstructUsing(e => new ResultatIndividuelPouleDto() { Libelle = e.Libelle, Lien = e.Lien });
            CreateMap<ResultatIndividuelClassement, ResultatIndividuelClassementDto>().ConstructUsing(e => new ResultatIndividuelClassementDto() { Classement = e.Classement, Club = e.Club, Nom = e.Nom, Points = e.Points, Rang = e.Rang });
            CreateMap<ResultatIndividuelPartie, ResultatIndividuelPartieDto>().ConstructUsing(e => new ResultatIndividuelPartieDto() { Forfait = e.Forfait, Libelle = e.Libelle, Perdant = e.Perdant, Vainqueur = e.Vainqueur });
        }
    }
}

using AutoMapper;
using WePing.domain.ResultatEquipeRencontres.Domain;
using WePing.domain.ResultatEquipeRencontres.Dto;

namespace WePing.domain.ResultatEquipeRencontres.Mapping
{
    public class ResultatEquipeRencontreProfile : Profile
    {
        public ResultatEquipeRencontreProfile()
        {
            CreateMap<ResultatEquipeRencontre, ResultatEquipeRencontreDto>().ConstructUsing(e => new ResultatEquipeRencontreDto() { Libelle = e.Libelle, EquipeA = e.EquipeA, EquipeB = e.EquipeB, ScoreA = e.ScoreA, ScoreB = e.ScoreB, Lien = e.Lien });
            CreateMap<ResultatEquipePoule, ResultatEquipePouleDto>().ConstructUsing(e => new ResultatEquipePouleDto() { Libelle = e.Libelle, Lien = e.Lien });
            CreateMap<ResultatEquipeClassement, ResultatEquipeClassementDto>().ConstructUsing(e => new ResultatEquipeClassementDto() { Classement = e.Classement, Equipe = e.Equipe, Joue = e.Joue, Numero = e.Numero, Points = e.Points, Poule = e.Poule });

        }
    }
}

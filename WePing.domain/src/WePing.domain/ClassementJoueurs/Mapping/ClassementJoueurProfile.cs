using AutoMapper;
using WePing.domain.ClassementJoueurs.Domain;
using WePing.domain.ClassementJoueurs.Dto;

namespace WePing.domain.ClassementJoueurs.Mapping
{
    public class ClassementJoueurProfile : Profile
    {
        public ClassementJoueurProfile()
        {
            CreateMap<ClassementJoueur, ClassementJoueurDto>().ConstructUsing(e => new ClassementJoueurDto() { Rang = e.Rang, Nom = e.Nom, Classement = e.Classement, Club = e.Club, Points = e.Points });

        }
    }
}

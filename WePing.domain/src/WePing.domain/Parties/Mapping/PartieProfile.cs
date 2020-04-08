using AutoMapper;
using WePing.domain.Parties.Domain;
using WePing.domain.Parties.Dto;

namespace WePing.domain.Parties.Mapping
{
    public class PartieProfile : Profile
    {
        public PartieProfile()
        {
            CreateMap<Partie, PartieDto>().
                ForMember(dest => dest.NomPrenomAdversaire, opt => opt.MapFrom(p => p.NomPrenomAdversaire ?? p.NomPrenomAdversaire_))
                .ForMember(dest => dest.VictoireDefaite, opt => opt.MapFrom(p => p.VictoireDefaite ?? p.VictoireDefaite_))
                .ForMember(dest => dest.ClassementAdversaire, opt => opt.MapFrom(p => p.ClassementAdversaire ?? p.ClassementAdversaire_))
                .ForMember(dest => dest.PointsGagnesPerdus, opt => opt.MapFrom(p => CalculatePoints(p)))
                ;
            /*CreateMap<Partie, PartieDto>().ConstructUsing(e => 
                new PartieDto() {
                    Licence = e.Licence,
                    LicenceAdversaire = e.LicenceAdversaire,
                    VictoireDefaite = (e.VictoireDefaite ?? e.VictoireDefaite_),
                    NumeroJournee = e.NumeroJournee,
                    Championnat = e.Championnat,
                    Date = e.Date,
                    SexeAdversaire = e.SexeAdversaire,
                    NomPrenomAdversaire = SetValue(e.NomPrenomAdversaire,e.NomPrenomAdversaire_),
                    PointsGagnesPerdus = e.PointsGagnesPerdus,
                    Coeficient = e.Coeficient,
                    ClassementAdversaire = (e.ClassementAdversaire ?? e.ClassementAdversaire_) 
                });*/


        }

        private object CalculatePoints(Partie p)
        {
            if (p.PointsGagnesPerdus == null)
            {
                return "XXX";
            }
            return p.PointsGagnesPerdus;
        }


    }
}

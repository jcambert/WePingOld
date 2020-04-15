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



        }

        private float CalculatePoints(Partie p)
        {
            /*if (p.PointsGagnesPerdus == null)
            {
                return 0.0f;
            }*/
            if (float.TryParse(p.PointsGagnesPerdus, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var result))
                return result;
            return 0.0f;


            //return p.PointsGagnesPerdus;
        }


    }
}

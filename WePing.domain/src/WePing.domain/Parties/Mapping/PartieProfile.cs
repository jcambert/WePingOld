using AutoMapper;
using System;
using System.Globalization;
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
                .ForMember(dest => dest.ClassementAdversaire, opt => opt.MapFrom(p =>Extensions.ToInt( p.ClassementAdversaire ?? p.ClassementAdversaire_,0)))
                .ForMember(dest => dest.PointsGagnesPerdus, opt => opt.MapFrom(p => Extensions.ToFloat(p.PointsGagnesPerdus,0.0f) /** ToFloat(p.Coeficient,1.0f)*/ ))
                .ForMember(dest=>dest.Coeficient,opt=>opt.MapFrom(p=> Extensions.ToFloat(p.Coeficient,1.0f)))
                .ForMember(dest=>dest.Date,opt=>opt.MapFrom(p=>Extensions.ToDate(p.Date)))
                ;



        }

        
    }
}

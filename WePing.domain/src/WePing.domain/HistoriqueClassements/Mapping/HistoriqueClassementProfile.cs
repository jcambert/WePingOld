using AutoMapper;
using WePing.domain.HistoriqueClassements.Domain;
using WePing.domain.HistoriqueClassements.Dto;

namespace WePing.domain.HistoriqueClassements.Mapping
{
    public class HistoriqueClassementProfile : Profile
    {
        public HistoriqueClassementProfile()
        {
            CreateMap<HistoriqueClassement, HistoriqueClassementDto>().ConstructUsing(e => new HistoriqueClassementDto() { Echelon = e.Echelon, Place = e.Place, Point = e.Point, Saison = e.Saison, Phase = e.Phase });

        }
    }
}

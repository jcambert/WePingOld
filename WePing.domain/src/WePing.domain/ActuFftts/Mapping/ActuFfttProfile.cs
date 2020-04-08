using AutoMapper;
using WePing.domain.ActuFftts.Domain;
using WePing.domain.ActuFftts.Dto;

namespace WePing.domain.ActuFftts.Mapping
{
    public class ActuFfttProfile : Profile
    {
        public ActuFfttProfile()
        {
            CreateMap<ActuFftt, ActuFfttDto>().ConstructUsing(e => new ActuFfttDto() { Date = e.Date, Titre = e.Titre, Description = e.Description, Url = e.Url, Photo = e.Photo });

        }
    }
}

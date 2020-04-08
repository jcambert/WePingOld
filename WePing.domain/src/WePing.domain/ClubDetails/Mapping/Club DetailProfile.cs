using AutoMapper;
using WePing.domain.ClubDetails.Domain;
using WePing.domain.ClubDetails.Dto;

namespace WePing.domain.ClubDetails.Mapping
{
    public class ClubDetailProfile : Profile
    {
        public ClubDetailProfile()
        {
            CreateMap<ClubDetail, ClubDetailDto>().ConstructUsing(e => new ClubDetailDto() { Id = e.Id, Numero = e.Numero, NomSalle = e.NomSalle, AdresseSalle1 = e.AdresseSalle1, AdresseSalle2 = e.AdresseSalle2, AdresseSalle3 = e.AdresseSalle3, CodePostalSalle = e.CodePostalSalle, VilleSalle = e.VilleSalle, Web = e.Web, NomCorrespondant = e.NomCorrespondant, PrenomCorrespondant = e.PrenomCorrespondant, MailCorrespondant = e.MailCorrespondant, TelephoneCorrespondant = e.TelephoneCorrespondant, Latitude = e.Latitude, Longitude = e.Longitude });

        }
    }
}

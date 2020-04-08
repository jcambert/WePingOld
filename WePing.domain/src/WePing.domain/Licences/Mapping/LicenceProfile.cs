using AutoMapper;
using WePing.domain.Licences.Domain;
using WePing.domain.Licences.Dto;

namespace WePing.domain.Licences.Mapping
{
    public class LicenceProfile : Profile
    {
        public LicenceProfile()
        {
            CreateMap<Licence, LicenceDto>().ConstructUsing(e => new LicenceDto() { Id = e.Id, Nom = e.Nom, Prenom = e.Prenom, Numero = e.Numero, NumeroClub = e.NumeroClub, Nomclub = e.Nomclub, Sexe = e.Sexe, Type = e.Type, CertificatMedical = e.CertificatMedical, Validation = e.Validation, Echelon = e.Echelon, Place = e.Place, Point = e.Point, Categorie = e.Categorie, AncienPointsMensuel = e.AncienPointsMensuel, PointsMensuel = e.PointsMensuel, ValeurInitial = e.ValeurInitial });

        }
    }
}

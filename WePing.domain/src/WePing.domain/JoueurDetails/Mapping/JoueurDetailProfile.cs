using AutoMapper;
using WePing.domain.JoueurDetails.Domain;
using WePing.domain.JoueurDetails.Dto;

namespace WePing.domain.JoueurDetails.Mapping
{
    public class JoueurDetailProfile : Profile
    {
        public JoueurDetailProfile()
        {
            CreateMap<JoueurDetail, JoueurDetailDto>().ConstructUsing(e => new JoueurDetailDto() { Licence = e.Licence, Nom = e.Nom, Prenom = e.Prenom, Club = e.Club, NumeroClub = e.NumeroClub, Nationalite = e.Nationalite, ClassementGlobal = e.ClassementGlobal, PointsMensuel = e.PointsMensuel, AncienClassementGlobal = e.AncienClassementGlobal, AncienPoints = e.AncienPoints, Classement = e.Classement, Categorie = e.Categorie, RangRegional = e.RangRegional, RangDepartemental = e.RangDepartemental, PointsOfficiels = e.PointsOfficiels, PropositionClassement = e.PropositionClassement, ValeurDebutSaison = e.ValeurDebutSaison });

        }
    }
}

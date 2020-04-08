using AutoMapper;
using WePing.domain.Joueurs.Domain;
using WePing.domain.Joueurs.Dto;

namespace WePing.domain.Joueurs.Mapping
{
    public class JoueurProfile : Profile
    {
        public JoueurProfile()
        {
            CreateMap<Joueur, JoueurDto>().ConstructUsing(e => new JoueurDto() { Classement = e.Classement, Nom = e.Nom, Prenom = e.Prenom, NomClub = e.NumeroClub, NumeroClub = e.NomClub });

        }
    }
}

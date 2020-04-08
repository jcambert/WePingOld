using MicroS_Common.Dto;
namespace WePing.domain.Joueurs.Dto
{
    public class JoueurDto : IDto
    {


        #region public properties

        public string Licence { get; set; }
        public string Classement { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string NomClub { get; set; }

        public string NumeroClub { get; set; }


        #endregion


    }
}

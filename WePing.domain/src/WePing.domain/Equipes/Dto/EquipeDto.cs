using System.Collections.Generic;
using WePing.domain.ResultatEquipeRencontres.Dto;

namespace WePing.domain.Equipes.Dto
{
    public class EquipeDto
    {


        #region public properties
        public string Nom { get; set; }

        public string Division { get; set; }

        public string Id { get; set; }

        public string Epreuve { get; set; }

        public string Lien { get; set; }

        public List< ResultatEquipeClassementDto> Classements { get; set; }

        public List<ResultatEquipeRencontreDto> Rencontres { get; set; }

        public (int,int) Classement { get; set; }

        public string  NumeroClub { get; set; }
        #endregion


    }
}

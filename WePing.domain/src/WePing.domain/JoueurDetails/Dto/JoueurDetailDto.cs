using MicroS_Common.Dto;
namespace WePing.domain.JoueurDetails.Dto
{
    public class JoueurDetailDto : BaseDto
    {


        #region public properties


        public string Licence { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Club { get; set; }

        public string NumeroClub { get; set; }

        public string Nationalite { get; set; }

        public string ClassementGlobal { get; set; }

        public string PointsMensuel { get; set; }

        public string AncienClassementGlobal { get; set; }

        public string AncienPoints { get; set; }

        public string Classement { get; set; }

        public string Categorie { get; set; }

        public string RangRegional { get; set; }

        public string RangDepartemental { get; set; }

        public string PointsOfficiels { get; set; }

        public string PropositionClassement { get; set; }

        public string ValeurDebutSaison { get; set; }


        public double NPointsMensuel => this.GetPoints(PointsMensuel);

        public double NAncienClassementGlobal => this.GetPoints(AncienClassementGlobal);

        public double NAncienPoints => this.GetPoints(AncienPoints);

        public double NPointsOfficiels => this.GetPoints(PointsOfficiels);
        #endregion


    }
}

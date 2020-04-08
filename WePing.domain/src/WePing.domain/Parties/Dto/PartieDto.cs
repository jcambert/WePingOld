namespace WePing.domain.Parties.Dto
{
    public class PartieDto
    {
        public PartieDto()
        {

        }

        #region public properties


        public string Licence { get; set; }

        public string LicenceAdversaire { get; set; }

        public string VictoireDefaite { get; set; }

        public string NumeroJournee { get; set; }

        public string Championnat { get; set; }

        public string Date { get; set; }

        public string SexeAdversaire { get; set; }

        public string NomPrenomAdversaire { get; set; }

        public string PointsGagnesPerdus { get; set; }

        public string Coeficient { get; set; }

        public string ClassementAdversaire { get; set; }

        #region @seexml_partie

        public string Epreuve { get; set; }
        public string Forfait { get; set; }
        #endregion


        public string PointsMensuel { get; set; }

        public string Points { get; set; }
        #endregion


    }
}

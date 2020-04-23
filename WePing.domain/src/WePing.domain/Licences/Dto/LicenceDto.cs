using MicroS_Common.Dto;
using Newtonsoft.Json;

namespace WePing.domain.Licences.Dto
{
    public class LicenceDto : IDto
    {


        #region public properties


        public string Id { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Numero { get; set; }

        public string NumeroClub { get; set; }

        public string Nomclub { get; set; }

        public string Sexe { get; set; }

        public string Type { get; set; }

        public string CertificatMedical { get; set; }

        public string Validation { get; set; }

        public string Echelon { get; set; }

        public string Place { get; set; }

        public string Point { get; set; }

        public string Categorie { get; set; }
        public string PointsMensuel { get; set; }
        public string AncienPointsMensuel { get; set; }
        public string ValeurInitial { get; set; }

        [JsonIgnore]
        public double NPoint => this.GetPoints(Point);
        [JsonIgnore]
        public double NPointsMensuel => this.GetPoints(PointsMensuel);
        [JsonIgnore]
        public double NAncienPointsMensuel => this.GetPoints(AncienPointsMensuel);
        [JsonIgnore]
        public double NValeurIntitial => this.GetPoints(ValeurInitial);

        public int Classement => (int)(NPointsMensuel / 100);

        public int ClassementOfficiel =>(int) ((System.DateTime.Now.Month>=9?PointsPhase1:PointsPhase2) /100);

        public int PointsMensuels => (int)(NValeurIntitial + NPointsMensuel - NValeurIntitial);
        public int PointsPhase1 => (int)(NValeurIntitial);
        public int PointsPhase2 => (int)(NPoint);
        public int ProgressionMensuelle => (int)(NPointsMensuel - NAncienPointsMensuel);
        public int ProgressionGenerale => (int)(NPointsMensuel - NValeurIntitial);
        public int ProgressionMensuelleCategorie => ProgressionMensuelle > 0 ? 1 : ProgressionMensuelle < 0 ? -1 : 0;
        public int ProgressionSaisonCategorie => ProgressionGenerale > 0 ? 1 : ProgressionGenerale < 0 ? -1 : 0;
        #endregion


    }
}

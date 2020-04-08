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
        #endregion


    }
}

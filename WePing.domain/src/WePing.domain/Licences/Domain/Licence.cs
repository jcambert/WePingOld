using System.Collections.Generic;
using System.Xml.Serialization;
namespace WePing.domain.Licences.Domain
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [XmlRoot(ElementName = "liste")]
    public class ListeLicences
    {
        [XmlElement(ElementName = "licence")]
        public List<Licence> Licences { get; set; } = new List<Licence>();
    }
    public class Licence
    {

        #region public properties
        [XmlElement(ElementName = "idlicence")]
        public string Id { get; set; }
        [XmlElement(ElementName = "nom")]
        public string Nom { get; set; }
        [XmlElement(ElementName = "prenom")]
        public string Prenom { get; set; }
        [XmlElement(ElementName = "licence")]
        public string Numero { get; set; }
        [XmlElement(ElementName = "numclub")]
        public string NumeroClub { get; set; }
        [XmlElement(ElementName = "nomclub")]
        public string Nomclub { get; set; }
        [XmlElement(ElementName = "sexe")]
        public string Sexe { get; set; }
        [XmlElement(ElementName = "type")]
        public string Type { get; set; }
        [XmlElement(ElementName = "certif")]
        public string CertificatMedical { get; set; }
        [XmlElement(ElementName = "validation")]
        public string Validation { get; set; }
        [XmlElement(ElementName = "echelon")]
        public string Echelon { get; set; }
        [XmlElement(ElementName = "place")]
        public string Place { get; set; }
        [XmlElement(ElementName = "point")]
        public string Point { get; set; }
        [XmlElement(ElementName = "cat")]
        public string Categorie { get; set; }
        [XmlElement(ElementName = "pointm")]
        public string PointsMensuel { get; set; }
        [XmlElement(ElementName = "apointm")]
        public string AncienPointsMensuel { get; set; }
        [XmlElement(ElementName = "initm")]
        public string ValeurInitial { get; set; }


        #endregion



    }
}

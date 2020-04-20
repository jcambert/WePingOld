using System.Collections.Generic;
using System.Xml.Serialization;
namespace WePing.domain.Parties.Domain
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [XmlRoot(ElementName = "liste")]
    public class ListeParties
    {
        [XmlElement(ElementName = "partie")]
        public List<Partie> Parties { get; set; } = new List<Partie>();
    }

    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [XmlRoot(ElementName = "liste")]
    public class ListeParties_
    {
        [XmlElement(ElementName = "resultat")]
        public List<Partie> Parties { get; set; } = new List<Partie>();
    }

    public class Partie
    {
        public Partie()
        {

        }

        #region public properties
        [XmlElement(ElementName = "licence")]
        public string Licence { get; set; }
        [XmlElement(ElementName = "advlic")]
        public string LicenceAdversaire { get; set; }
        [XmlElement(ElementName = "vd")]
        public string VictoireDefaite { get; set; }
        [XmlElement(ElementName = "victoire")]
        public string VictoireDefaite_ { get; set; }
        [XmlElement(ElementName = "numjourn")]
        public string NumeroJournee { get; set; }
        [XmlElement(ElementName = "codechamp")]
        public string Championnat { get; set; }
        [XmlElement(ElementName = "date")]
        public string Date { get; set; }
        [XmlElement(ElementName = "advsexe")]
        public string SexeAdversaire { get; set; }
        [XmlElement(ElementName = "advnompre")]
        public string NomPrenomAdversaire { get; set; }
        [XmlElement(ElementName = "nom")]
        public string NomPrenomAdversaire_ { get; set; }
        [XmlElement(ElementName = "pointres")]
        public string PointsGagnesPerdus { get; set; }
        [XmlElement(ElementName = "coefchamp")]
        public string Coeficient { get; set; }
        [XmlElement(ElementName = "advclaof")]
        public string ClassementAdversaire { get; set; }
        [XmlElement(ElementName = "classement")]
        public string ClassementAdversaire_ { get; set; }

        [XmlElement(ElementName = "epreuve")]
        public string Epreuve { get; set; }

        [XmlElement(ElementName = "forfait")]
        public string Forfait { get; set; }
        [XmlElement(ElementName ="idpartie")]
        public int IdPartie { get; set; }
        [XmlElement(IsNullable = true)]
        public string PointsMensuel { get; set; } = null;
        [XmlElement(IsNullable = true)]
        public string Points { get; set; } = null;
        #endregion



    }
}

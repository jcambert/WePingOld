using System.Collections.Generic;
using System.Xml.Serialization;

namespace WePing.domain.ClassementJoueurs.Domain
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [XmlRoot(ElementName = "liste")]
    public class ListeClassementJoueur
    {
        [XmlElement(ElementName = "club")]
        public List<ClassementJoueur> Classements { get; set; } = new List<ClassementJoueur>();
    }
    public class ClassementJoueur
    {

        #region public properties
        [XmlElement(ElementName = "rang")]
        public string Rang { get; set; }
        [XmlElement(ElementName = "nom")]
        public string Nom { get; set; }
        [XmlElement(ElementName = "clt")]
        public string Classement { get; set; }
        [XmlElement(ElementName = "club")]
        public string Club { get; set; }
        [XmlElement(ElementName = "points")]
        public string Points { get; set; }

        #endregion



    }
}

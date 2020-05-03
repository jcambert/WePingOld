using System.Collections.Generic;
using System.Xml.Serialization;

namespace WePing.domain.Rencontres.Domain
{
    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRoot(ElementName = "liste")]
    public class Rencontre
    {
        #region public properties
        [XmlElement(ElementName = "resultat")]
        public ResultatRencontre Resultat { get; set; }

        [XmlElement(ElementName = "joueur")]
        public List<JoueurRencontre> Joueurs { get; set; }

        [XmlElement(ElementName = "partie")]
        public List<PartieRencontre> Parties { get; set; }
        #endregion

    }

    public class ResultatRencontre
    {
        [XmlElement("equa")]
        public string EquipeA { get; set; }
        [XmlElement("equb")]
        public string EquipeB { get; set; }
        [XmlElement("resa")]
        public string ResultatA { get; set; }
        [XmlElement("resb")]
        public string ResultatB { get; set; }
    }

    public class JoueurRencontre
    {
        [XmlElement("xja")]  
        public string JoueurA { get; set; }
        [XmlElement("xjb")]  
        public string JoueurB { get; set; }
        [XmlElement("xca")]  
        public string ClassementA { get; set; }
        [XmlElement("xcb")]  
        public string ClassementB { get; set; }
    }

    public class PartieRencontre
    {
        [XmlElement("ja")]
        public string JoueurA { get; set; }
        [XmlElement("jb")]
        public string JoueurB { get; set; }
        [XmlElement("scorea")]
        public string ScoreA { get; set; }
        [XmlElement("scoreb")]
        public string ScoreB { get; set; }
        [XmlElement("detail")]
        public string Detail { get; set; }

    }
}

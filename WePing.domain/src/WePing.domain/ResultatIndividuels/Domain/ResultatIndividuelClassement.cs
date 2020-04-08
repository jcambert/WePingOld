using System.Collections.Generic;
using System.Xml.Serialization;
namespace WePing.domain.ResultatIndividuels.Domain
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [XmlRoot(ElementName = "liste")]
    public class ListeResultatIndividuelClassement
    {
        [XmlElement(ElementName = "classement")]
        public List<ResultatIndividuelClassement> Classements { get; set; } = new List<ResultatIndividuelClassement>();
    }
    public class ResultatIndividuelClassement
    {


        public ResultatIndividuelClassement()
        {

        }
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

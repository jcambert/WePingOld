using System.Collections.Generic;
using System.Xml.Serialization;
namespace WePing.domain.HistoriqueClassements.Domain
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [XmlRoot(ElementName = "liste")]
    public class ListeHistoriqueClassements
    {
        [XmlElement(ElementName = "histo")]
        public List<HistoriqueClassement> Historiques { get; set; } = new List<HistoriqueClassement>();
    }
    public class HistoriqueClassement
    {

        #region public properties
        [XmlElement(ElementName = "echelon")]
        public string Echelon { get; set; }
        [XmlElement(ElementName = "place")]
        public string Place { get; set; }
        [XmlElement(ElementName = "point")]
        public string Point { get; set; }
        [XmlElement(ElementName = "saison")]
        public string Saison { get; set; }
        [XmlElement(ElementName = "phase")]
        public string Phase { get; set; }

        #endregion



    }
}

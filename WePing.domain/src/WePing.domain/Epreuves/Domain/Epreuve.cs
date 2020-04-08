using System.Collections.Generic;
using System.Xml.Serialization;
namespace WePing.domain.Epreuves.Domain
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [XmlRoot(ElementName = "liste")]
    public class ListeEpreuve
    {
        [XmlElement(ElementName = "epreuve")]
        public List<Epreuve> Epreuves { get; set; }
    }
    public class Epreuve
    {

        public Epreuve()
        {

        }
        #region public properties
        [XmlElement(ElementName = "idepreuve")]
        public string Id { get; set; }
        [XmlElement(ElementName = "idorga")]
        public string Organisme { get; set; }
        [XmlElement(ElementName = "libelle")]
        public string Libelle { get; set; }

        #endregion

    }
}

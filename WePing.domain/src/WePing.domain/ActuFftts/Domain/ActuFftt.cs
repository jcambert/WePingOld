using System.Collections.Generic;
using System.Xml.Serialization;
namespace WePing.domain.ActuFftts.Domain
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [XmlRoot(ElementName = "liste")]
    public class ListeActualites
    {
        [XmlElement(ElementName = "news")]
        public List<ActuFftt> Actualites { get; set; } = new List<ActuFftt>();
    }
    public class ActuFftt
    {

        #region public properties
        [XmlElement(ElementName = "date")]
        public string Date { get; set; }
        [XmlElement(ElementName = "titre")]
        public string Titre { get; set; }
        [XmlElement(ElementName = "description")]
        public string Description { get; set; }
        [XmlElement(ElementName = "url")]
        public string Url { get; set; }
        [XmlElement(ElementName = "photo")]
        public string Photo { get; set; }

        #endregion



    }
}

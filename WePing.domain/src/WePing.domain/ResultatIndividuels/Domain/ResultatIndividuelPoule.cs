using System.Collections.Generic;
using System.Xml.Serialization;
namespace WePing.domain.ResultatIndividuels.Domain
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [XmlRoot(ElementName = "liste")]
    public class ListeResultatIndividuelPoule
    {
        [XmlElement(ElementName = "tour")]
        public List<ResultatIndividuelPoule> Tours { get; set; } = new List<ResultatIndividuelPoule>();
    }
    public class ResultatIndividuelPoule
    {


        public ResultatIndividuelPoule()
        {

        }
        #region public properties
        [XmlElement(ElementName = "libelle")]
        public string Libelle { get; set; }
        [XmlElement(ElementName = "lien")]
        public string Lien { get; set; }

        #endregion
    }
}

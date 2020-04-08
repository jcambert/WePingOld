using System.Collections.Generic;
using System.Xml.Serialization;
namespace WePing.domain.ResultatIndividuels.Domain
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [XmlRoot(ElementName = "liste")]
    public class ListeResultatIndividuelPartie
    {
        [XmlElement(ElementName = "partie")]
        public List<ResultatIndividuelPartie> Parties { get; set; } = new List<ResultatIndividuelPartie>();
    }
    public class ResultatIndividuelPartie
    {


        public ResultatIndividuelPartie()
        {

        }
        #region public properties
        [XmlElement(ElementName = "libelle")]
        public string Libelle { get; set; }
        [XmlElement(ElementName = "vain")]
        public string Vainqueur { get; set; }
        [XmlElement(ElementName = "perd")]
        public string Perdant { get; set; }
        [XmlElement(ElementName = "forfait")]
        public string Forfait { get; set; }
        #endregion
    }
}

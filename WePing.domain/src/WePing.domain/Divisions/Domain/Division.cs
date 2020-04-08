using System.Collections.Generic;
using System.Xml.Serialization;
namespace WePing.domain.Divisions.Domain
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [XmlRoot(ElementName = "liste")]
    public class ListeDivisions
    {
        [XmlElement(ElementName = "division")]
        public List<Division> Divisions { get; set; } = new List<Division>();
    }
    public class Division
    {

        public Division()
        {

        }
        #region public properties

        [XmlElement(ElementName = "iddivision")]
        public string Id { get; set; }
        [XmlElement(ElementName = "libelle")]
        public string Libelle { get; set; }

        #endregion



    }
}

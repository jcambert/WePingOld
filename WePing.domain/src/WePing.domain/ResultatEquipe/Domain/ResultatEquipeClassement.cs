using System.Collections.Generic;
using System.Xml.Serialization;
namespace WePing.domain.ResultatEquipeRencontres.Domain
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [XmlRoot(ElementName = "liste")]
    public class ListeResultatEquipeClassements
    {
        [XmlElement(ElementName = "classement")]
        public List<ResultatEquipeClassement> Classements { get; set; } = new List<ResultatEquipeClassement>();
    }
    public class ResultatEquipeClassement
    {
        public ResultatEquipeClassement()
        {

        }
        #region public properties
        [XmlElement(ElementName = "poule")]
        public string Poule { get; set; }
        [XmlElement(ElementName = "clt")]
        public string Classement { get; set; }
        [XmlElement(ElementName = "equipe")]
        public string Equipe { get; set; }
        [XmlElement(ElementName = "joue")]
        public string Joue { get; set; }
        [XmlElement(ElementName = "pts")]
        public string Points { get; set; }
        [XmlElement(ElementName = "numero")]
        public string Numero { get; set; }

        #endregion

    }
}

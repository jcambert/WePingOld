using System.Xml.Serialization;
namespace WePing.domain.JoueurDetails.Domain
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [XmlRoot(ElementName = "liste")]
    public class ListeJoueurDetails
    {
        [XmlElement(ElementName = "joueur")]
        public JoueurDetail Joueur { get; set; } = new JoueurDetail();
    }
    public class JoueurDetail
    {

        #region public properties
        [XmlElement(ElementName = "licence")]
        public string Licence { get; set; }
        [XmlElement(ElementName = "nom")]
        public string Nom { get; set; }
        [XmlElement(ElementName = "prenom")]
        public string Prenom { get; set; }
        [XmlElement(ElementName = "club")]
        public string Club { get; set; }
        [XmlElement(ElementName = "nclub")]
        public string NumeroClub { get; set; }
        [XmlElement(ElementName = "natio")]
        public string Nationalite { get; set; }
        [XmlElement(ElementName = "clglob")]
        public string ClassementGlobal { get; set; }
        [XmlElement(ElementName = "point")]
        public string PointsMensuel { get; set; }
        [XmlElement(ElementName = "aclglob")]
        public string AncienClassementGlobal { get; set; }
        [XmlElement(ElementName = "apoint")]
        public string AncienPoints { get; set; }
        [XmlElement(ElementName = "clast")]
        public string Classement { get; set; }
        [XmlElement(ElementName = "categ")]
        public string Categorie { get; set; }
        [XmlElement(ElementName = "rangreg")]
        public string RangRegional { get; set; }
        [XmlElement(ElementName = "rangdep")]
        public string RangDepartemental { get; set; }
        [XmlElement(ElementName = "valcla")]
        public string PointsOfficiels { get; set; }
        [XmlElement(ElementName = "clpro")]
        public string PropositionClassement { get; set; }
        [XmlElement(ElementName = "valinit")]
        public string ValeurDebutSaison { get; set; }

        #endregion



    }
}

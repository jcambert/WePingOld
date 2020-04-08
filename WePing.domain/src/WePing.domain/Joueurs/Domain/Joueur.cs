using System.Collections.Generic;
using System.Xml.Serialization;
namespace WePing.domain.Joueurs.Domain
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [XmlRoot(ElementName = "liste")]
    public class ListeJoueurs
    {
        [XmlElement(ElementName = "joueur")]
        public List<Joueur> Joueurs { get; set; } = new List<Joueur>();
    }
    public class Joueur
    {

        #region public properties

        [XmlElement(ElementName = "licence")]
        public string Licence { get; set; }


        [XmlElement(ElementName = "nom")]
        public string Nom { get; set; }


        [XmlElement(ElementName = "prenom")]
        public string Prenom { get; set; }


        [XmlElement(ElementName = "nclub")]
        public string NumeroClub { get; set; }


        [XmlElement(ElementName = "club")]
        public string NomClub { get; set; }

        [XmlElement(ElementName = "clast")]
        public string Classement { get; set; }
        #endregion



    }
}

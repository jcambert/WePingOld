using System.ComponentModel;
using WePing.domain.Clubs.Queries;
using WePing.domain.Joueurs.Queries;
using WePing.domain.Licences.Queries;
namespace WePing.Components
{
    public enum SearchType
    {

        [Description("Tous")]
        All,

        [Description("Joueur")]
        [Query(typeof(BrowseJoueur))]
        //[Api((query)=>())]
        Player,

        [Description("Licence")]
        [Query(typeof(GetLicence))]
        Licence,

        [Query(typeof(BrowseClubs))]
        [Description("Club")]
        Club
    }
}

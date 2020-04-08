using MicroS_Common;
using MicroS_Common.Types;
using WePing.domain.Joueurs.Dto;
namespace WePing.domain.Joueurs.Queries
{

    public class BrowseJoueur : PagedQueryBase, IQuery<PagedResult<JoueurDto>>
    {
        public string Club { get; set; }
        [Default]
        [ToUpperCaseFormat]
        public string Nom { get; set; }
        [ToTileCaseFormat]
        public string Prenom { get; set; }
        //public string Licence { get; set; }
        //[DisableSearchFilter]
       // public string Valid { get; set; } = "O";
    }
}

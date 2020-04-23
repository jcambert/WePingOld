using System;
using System.Collections.Generic;
using System.Linq;
using WePing.domain.ClubDetails.Dto;
using WePing.domain.Clubs.Dto;
using WePing.domain.Equipes.Dto;
using WePing.domain.HistoriqueClassements.Dto;
using WePing.domain.JoueurDetails.Dto;
using WePing.domain.Joueurs.Dto;
using WePing.domain.Licences.Dto;
using WePing.domain.Organismes.Dto;
using WePing.domain.Parties.Dto;
using WePing.domain.Profiles.Dto;
using WeReduxBlazor;

namespace WePing.Services
{
    internal struct LicenceCategorie
    {
        public string Categorie { get; set; }
        public int Total { get; set; }
        public int Pourcentage { get; set; }
    }

    internal struct LicenceProgressionMensuelle
    {
        public string Categorie { get; set; }
        public int Total { get; set; }
        public int Pourcentage { get; set; }

    }
    internal struct LicenceProgressionSaison
    {
        public string Categorie { get; set; }
        public int Total { get; set; }
        public int Pourcentage { get; set; }

    }

    internal struct LicenceClassement
    {
        public int Classement { get; set; }
        public int Total { get; set; }
        public int Pourcentage { get; set; }
    }
    
    public class WePingState : StateBase
    {
        public ProfileDto Profile { get; set; }
        public IPagedResultWithLinks<LicenceDto> Licences { get; set; } = PagedResultWithLinks<LicenceDto>.Blank();
        public IPagedResultWithLinks<JoueurDto> Joueurs { get; set; } = PagedResultWithLinks<JoueurDto>.Blank();
        public IPagedResultWithLinks<HistoriqueClassementDto> HistoriqueClassement { get; set; } = PagedResultWithLinks<HistoriqueClassementDto>.Blank();

        public IPagedResultWithLinks<ClubDto> Clubs { get; set; } = PagedResultWithLinks<ClubDto>.Blank();

        public IPagedResultWithLinks<LicenceDto> LicencesForClubs { get; set; } = PagedResultWithLinks<LicenceDto>.Blank();

        public IPagedResultWithLinks<EquipeDto> Equipes { get; set; } = PagedResultWithLinks<EquipeDto>.Blank();
        public IPagedResultWithLinks<OrganismeDto> Organismes { get; set; } = PagedResultWithLinks<OrganismeDto>.Blank();
        public IPagedResultWithLinks<PartieDto> Parties { get; set; } = PagedResultWithLinks<PartieDto>.Blank();



        public ClubDetailDto ClubDetail { get; set; } = new ClubDetailDto();
        public ClubDto Club { get; set; } = new ClubDto();

        public LicenceDto Licence { get; set; }

        public JoueurDetailDto Joueur { get; set; }

        internal List<EquipeDto> EquipePhase1 => Equipes.Items.Where(e => e.Nom.ToLower().Contains("phase 1")).ToList();
        internal List<EquipeDto> EquipePhase2 => Equipes.Items.Where(e => e.Nom.ToLower().Contains("phase 2")).ToList();


        internal List<LicenceCategorie> LicenciesParCategorie
        {
            get
            {
                var cats = from lic in LicencesForClubs.Items orderby lic.Categorie group lic by lic.Categorie into g select new LicenceCategorie() { Categorie = g.Key, Total = g.Count(), Pourcentage = (int)Math.Floor(1.0 * g.Count() / LicencesForClubs.TotalResults * 100) };
                var res = cats.ToList();
                return res;
            }
        }

        internal List<LicenceProgressionMensuelle> LicenciesParProgressionMensuelle
        {
            get
            {
                var l = from lic in LicencesForClubs.Items orderby lic.ProgressionMensuelle descending group lic by lic.ProgressionMensuelleCategorie into g select new LicenceProgressionMensuelle() { Categorie = (g.Key == 1 ? "Positive" : g.Key == -1 ? "Negative" : "Nulle"), Total = g.Count(), Pourcentage = (int)Math.Floor(1.0 * g.Count() / LicencesForClubs.TotalResults * 100) };
                return l.ToList();
            }
        }
        internal List<LicenceProgressionSaison> LicenciesParProgressionSaison
        {
            get
            {
                var l = from lic in LicencesForClubs.Items orderby lic.ProgressionSaisonCategorie descending group lic by lic.ProgressionSaisonCategorie into g select new LicenceProgressionSaison() { Categorie = (g.Key == 1 ? "Positive" : g.Key == -1 ? "Negative" : "Nulle"), Total = g.Count(), Pourcentage = (int)Math.Floor(1.0 * g.Count() / LicencesForClubs.TotalResults * 100) };
                return l.ToList();
            }
        }

        internal List<LicenceClassement> LicenciesParPointsMensuels
        {
            get
            {
                var l = from lic in LicencesForClubs.Items orderby lic.Classement descending group lic by lic.Classement into g select new LicenceClassement() { Classement = g.Key, Total = g.Count(), Pourcentage = (int)Math.Floor(1.0 * g.Count() / LicencesForClubs.TotalResults * 100) };
                return l.ToList();
            }
        }

        internal List<LicenceClassement> LicenciesParClassement
        {
            get
            {
                var l = from lic in LicencesForClubs.Items orderby lic.ClassementOfficiel descending group lic by lic.ClassementOfficiel into g select new LicenceClassement() { Classement=g.Key, Total = g.Count(), Pourcentage = (int)Math.Floor(1.0 * g.Count() / LicencesForClubs.TotalResults * 100) };
                return l.ToList();
            }
        }
    }


}

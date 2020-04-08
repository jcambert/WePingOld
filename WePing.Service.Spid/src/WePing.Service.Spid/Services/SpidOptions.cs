
using MicroS_Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace WePing.Service.Spid.Services
{
    public class SpidEndPoints
    {
        public string EndPoint { get; set; }

        public Dictionary<string, string> Api { get; set; }

        public string GetApi(string point) => EndPoint + string.Format(Api["endpoint"], Api[point]);
    }
    public sealed class SpidOptions
    {
        private readonly SpidEndPoints _endpoints;
        private readonly ILogger<SpidOptions> _logger;

        internal const string SPID_OPTION = "spid";
        internal const string CLUB_DETAIL = "club_detail";
        internal const string CLUB_LISTE = "club_liste";
        internal const string DIVISION = "division";
        internal const string EPREUVES = "epreuves";
        internal const string ORGANISMES = "organismes";
        internal const string RESULTAT_EQUIPE_RENCONTRES = "resultat_equipe";
        internal const string RESULTAT_EQUIPE_POULES = "resultat_equipe";
        internal const string RESULTAT_EQUIPE_CLASSEMENTS = "resultat_equipe";
        internal const string EQUIPES = "equipe_liste";
        internal const string RESULTAT_INDIVIDUEL_POULES = "resultat_indiv";
        internal const string RESULTAT_INDIVIDUEL_CLASSEMENTS = "resultat_indiv";
        internal const string RESULTAT_INDIVIDUEL_PARTIES = "resultat_indiv";
        internal const string CLASSEMENT_JOUEUR = "classement";
        internal const string JOUEURS = "joueur_liste_cla";
        internal const string JOUEUR_DETAIL = "joueur_detail_cla";
        internal const string LICENCE = "joueur_licence_spid";
        internal const string PARTIES = "joueur_partie_mysql";
        internal const string PARTIES_ = "joueur_partie_spid";
        internal const string ACTUALITES = "actu_fftt";
        internal const string HISTO_CLASSEMENT = "joueur_historique_cla";
        public SpidOptions(IConfiguration config, ILogger<SpidOptions> logger)
        {
            this._logger = logger;
            try
            {
                this._endpoints = config.GetOptions<SpidEndPoints>(SPID_OPTION);
            }
            catch (Exception e)
            {
                _logger.LogError($"YOU MUST DEFINE SPID SECTION IN YOUR appsettings.json file\n\t{e.Message}");
            }
        }

        public string EndPoint
        {
            get
            {
                string res = string.Empty;
                try
                {
                    res = this._endpoints?.EndPoint ?? "";
                    if (res == "")
                        _logger.LogWarning("The spid endpoint was not defined in spid section in appsetting.json");
                }
                catch (Exception e)
                {
                    _logger.LogError($"The spid endpoint was not defined in spid section in appsetting.json\n\t{e.Message}");
                }

                return res;

            }
        }

        public string this[string name]
        {
            get
            {
                string res;
                try
                {
                    res = this._endpoints?.GetApi(name) ?? "";
                    if (res == "")
                        _logger.LogWarning($"This SpidOption was not defined in spid section in appsetting.json:{name}");
                }
                catch (Exception e)
                {
                    var message = $"This SpidOption was not defined in spid section in appsetting.json:{name}  -> {e.Message}";
                    _logger.LogError(message);
                    throw new Exception(message);
                }
                return res;
            }
        }
    }
}

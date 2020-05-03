using MicroS_Common.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using WePing.Components;
using WePing.domain.Equipes.Dto;
using WePing.domain.JoueurDetails.Dto;
using WePing.domain.Licences.Dto;
using WePing.domain.ResultatEquipeRencontres.Dto;
using WePing.Services;
namespace WePing
{
    public class QueryAttribute : Attribute
    {
        readonly Type _type;
        public QueryAttribute(Type t)
        {
            if (!typeof(IQuery).IsAssignableFrom(t))
                throw new Exception("QueryAttribute must be an IQuery type");
            if (t.IsAbstract)
                throw new Exception("QueryAttribute Type must not be an abstract class");
            if (t.GetConstructor(new Type[] { }) == null)
                throw new Exception("QueryAttribute must have an empty constructor");

            this._type = t;

        }

        public Type QueryType => _type;

        public IQuery Create => _type.GetConstructor(new Type[] { }).Invoke(new Type[] { }) as IQuery;
    }
    public static class Extensions
    {

        internal static void AddDisposable(this IDisposable disposable, List<IDisposable> registry)
        {
            registry?.Add(disposable);
        }
        public static Type GetQueryType(this SearchType type)
        {
            return type.GetType().GetMember(type.ToString())
                   .First()
                   .GetCustomAttribute<QueryAttribute>()?
                   .QueryType ?? null;
        }

        public static IQuery GetQuery(this SearchType type) =>
            type.GetType().GetMember(type.ToString())
                   .First().GetCustomAttribute<QueryAttribute>()?.Create;

        private static string GetIdValue(this string value, string pattern)
        {
            var regex = new Regex(pattern);
            var match = regex.Match(value);
            return match.Success && match.Groups["id"].Success ? match.Groups["id"].Value : string.Empty;
        }

        private static string GetLienValue(this EquipeDto equipe, string pattern) => equipe?.Lien.GetIdValue(pattern) ?? string.Empty;
        public static string GetCodeOrganisme(this EquipeDto equipe) => equipe?.GetLienValue(@"organisme_pere=(?<id>\d+)") ?? string.Empty;

        public static string GetPouleId(this EquipeDto equipe) => equipe?.GetLienValue(@"cx_poule=(?<id>\d+)") ?? string.Empty;

        public static string GetDivision(this EquipeDto equipe) => equipe?.GetLienValue(@"D1=(?<id>\d+)") ?? string.Empty;

        public static (string, string, DateTime) GetPouleInformations(this ResultatEquipeRencontreDto resultat)
        {

            var libelle = resultat?.Libelle.ToLower().Replace(" ", "") ?? string.Empty;
            if (string.IsNullOrEmpty(libelle)) return ("", "", DateTime.Now);
            var poule_pattern = @"poule(?<id>\w+)";
            var journee_pattern = @"tourn°(?<id>\d+)";
            var date_pattern = @"(?<id>((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2})))))";
            DateTime dt;
            if (!DateTime.TryParse(libelle.GetIdValue(date_pattern), out dt))
                dt = DateTime.Now;
            return (libelle.GetIdValue(poule_pattern), libelle.GetIdValue(journee_pattern), dt);


        }

        public static (int, int) GetClassement(this EquipeDto equipe, string numeroClub)
        {
            try
            {
                var res = from clt in equipe.Classements where clt.Numero == numeroClub select (Int32.Parse(clt.Classement), equipe.Classements.Count);
                return res.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Debugger.Break();
                return (0, 0);
            }
        }

        private static System.Collections.Generic.Dictionary<string, string> categories = new System.Collections.Generic.Dictionary<string, string>()
        {
            { "P","Poussin" },
            {"B1","Benjamin 1" },
            {"B2","Benjamin 1" },
            {"M1","Minime 1" },
            {"M2","Minime 2" },
            {"C1","Cadet 1" },
            {"C2","Cadet 2" },
            {"J1","Junior 1" },
            {"J2","Junior 2" },
            {"J3","Junior3" },
            {"S","Senior" },
            {"V1","Vétéran 1" },
            {"V2","Vétéran 2" },
            {"V3","Vétéran 3" },
            {"V4","Vétéran 4" },
            {"V5","Vétéran 5" },
        };
        internal static string GetCategorie(this LicenceCategorie lic)
        => lic.Categorie == null ? "" : categories[lic.Categorie];
        internal static string GetCategorie(this JoueurDetailDto lic)
       => lic.Categorie == null ? "" : categories[lic.Categorie];
        internal static string GetCategorie(this LicenceDto lic)
       => lic.Categorie == null ? "" : categories[lic.Categorie];


    }
}

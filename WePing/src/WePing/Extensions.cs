using MicroS_Common.Types;
using OfficeOpenXml.FormulaParsing.ExpressionGraph;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using WePing.Components;
using WePing.domain.Equipes.Dto;
using WePing.domain.Licences.Dto;

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

        private static string GetPatternedValue(this EquipeDto equipe, string pattern)
        {
            var regex = new Regex(pattern);
            var match = regex.Match(equipe.Lien);
            return match.Success && match.Groups["id"].Success ? match.Groups["id"].Value : string.Empty;
        }
        public static string GetCodeOrganisme(this EquipeDto equipe) => equipe.GetPatternedValue(@"organisme_pere=(?<id>\d+)");

        public static string GetCxPoule(this EquipeDto equipe) => equipe.GetPatternedValue(@"cx_poule=(?<id>\d+)");

        public static string GetD1(this EquipeDto equipe) => equipe.GetPatternedValue(@"D1=(?<id>\d+)");

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
        internal static string GetCategorie(this WePing.Services.LicenceCategorie lic)
        => categories[lic.Categorie];

        
    }
}

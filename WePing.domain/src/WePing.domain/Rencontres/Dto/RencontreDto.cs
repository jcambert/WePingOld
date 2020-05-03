using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WePing.domain.Rencontres.Dto
{
   
    public class RencontreDto
    {

        #region public properties
        public ResultatRencontreDto Resultat { get; set; }

        public List<JoueurRencontreDto> Joueurs { get; set; }

        public List<PartieRencontreDto> Parties { get; set; }

        public int NumberOfSetsA { get; set; }
        public int NumberOfSetsB { get; set; }

        public StatutRencontre StatutA => NumberOfSetsA > NumberOfSetsB ? StatutRencontre.Gagne : NumberOfSetsA < NumberOfSetsB ? StatutRencontre.Perdu : StatutRencontre.Nul;

        public StatutRencontre StatutB => NumberOfSetsB > NumberOfSetsA ? StatutRencontre.Gagne : NumberOfSetsB < NumberOfSetsA ? StatutRencontre.Perdu : StatutRencontre.Nul;

        #endregion


    }

    public class ResultatRencontreDto
    {
        public string EquipeA { get; set; }
        public string EquipeB { get; set; }
        public string ResultatA { get; set; }
        public string ResultatB { get; set; }

        public int NResultatA
        {
            get
            {
                int res = 0;
                Int32.TryParse(ResultatA, out res);
                return res;

            }
        }
        public int NResultatB
        {
            get
            {
                int res = 0;
                Int32.TryParse(ResultatB, out res);
                return res;

            }
        }
        public StatutRencontre StatutEquipeA => NResultatA > NResultatB ? StatutRencontre.Gagne : NResultatA < NResultatB ? StatutRencontre.Perdu : StatutRencontre.Nul;
        public StatutRencontre StatutEquipeB => NResultatB > NResultatA ? StatutRencontre.Gagne : NResultatB < NResultatA ? StatutRencontre.Perdu : StatutRencontre.Nul;
    }

    public class JoueurRencontreDto
    {
        public string JoueurA { get; set; }
        public string JoueurB { get; set; }
        public string ClassementA { get; set; }
        public string ClassementB { get; set; }
        public string SexeA { get;  set; }
        public int PointsA { get;  set; }
        public string SexeB { get;  set; }
        public int PointsB { get;  set; }
        public double PointsGagnesPerdusA { get; set; }
        public double PointsGagnesPerdusB { get; set; }
    }

    public class PartieRencontreDto
    {
        public string JoueurA { get; set; }
        public string JoueurB { get; set; }
        public string ScoreA { get; set; }
        public string ScoreB { get; set; }
        public string Detail { get; set; }
        public double PointsA { get; set; }
        public double PointsB { get; set; }
        public double PointsGagnesPerdusA { get; set; }
        public double PointsGagnesPerdusB { get; set; }
        public StatutRencontre StatutJoueurA => ScoreA=="1" ? StatutRencontre.Gagne : StatutRencontre.Perdu;
        public StatutRencontre StatutJoueurB => ScoreB == "1" ? StatutRencontre.Gagne : StatutRencontre.Perdu;
        public List<(string, StatutRencontre)> GetDetail()
        {
            List<(string, StatutRencontre)> res = new List<(string, domain.StatutRencontre)>();
            var pts = Detail.Split(" ").ToList().Select(x => (Int32.Parse(x)));
            pts.ToList().ForEach(x =>
            {
                var offset = 0;
                if (Math.Abs(x) == 10)
                    offset = 1;
                else if (Math.Abs(x) > 10)
                    offset = 2;
                var s = x > 0 ? $"{11 + offset}-{x}" : $"{Math.Abs(x)}-{11 + offset}";
                res.Add((s, x > 0 ? StatutRencontre.Gagne : StatutRencontre.Perdu));
                if (x > 0)
                    NumberOfSetsA++;
                else
                    NumberOfSetsB++;
            });
            return res;
        }
        public int NumberOfSetsA { get; set; }
        public int NumberOfSetsB { get; set; }
    }
}

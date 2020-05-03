using AutoMapper;
using System;
using System.Text.RegularExpressions;
using WePing.domain.Rencontres.Domain;
using WePing.domain.Rencontres.Dto;

namespace WePing.domain.Rencontres.Mapping
{
    public class RencontreProfile : Profile
    {
        public RencontreProfile()
        {
            CreateMap<Rencontre, RencontreDto>();
            CreateMap<ResultatRencontre, ResultatRencontreDto>();
            CreateMap<JoueurRencontre, JoueurRencontreDto>().AfterMap((src,dest)=> {
                var clta = GetClassement(dest.ClassementA);
                var cltb = GetClassement(dest.ClassementB);
                dest.SexeA = clta.Item1;
                dest.PointsA = clta.Item2;
                dest.SexeB = cltb.Item1;
                dest.PointsB = cltb.Item2;

            });
            CreateMap<PartieRencontre, PartieRencontreDto>();

        }

        private (string, int) GetClassement(string value)
        {
            // if (value == "F 512pts") System.Diagnostics.Debugger.Break();
            int result = 0;
            string pattern = @"^(?<sexe>[f]|[m]|[F]|[M]) (?<points>\d+)pts";
            var regex = new Regex(pattern);
            var match = regex.Match(value);
            Int32.TryParse(match.Success && match.Groups["points"].Success ? match.Groups["points"].Value : string.Empty, out result);
            var sexe = match.Success && match.Groups["sexe"].Success ? match.Groups["sexe"].Value : "M";
            return (sexe, result);
        }
    }
}

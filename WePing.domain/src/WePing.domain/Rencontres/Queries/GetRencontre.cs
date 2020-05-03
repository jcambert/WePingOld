using MicroS_Common.Types;
using System;
using WePing.domain.Rencontres.Dto;

namespace WePing.domain.Rencontres.Queries
{
    public class GetRencontreLien : IQuery<RencontreDto>
    {
        public string Lien { get; set; }
    }
    public class GetRencontre : IQuery<RencontreDto>
    {

        public string Is_Retour { get; set; }
        public string Phase { get; set; }
        public string Res_1 { get; set; }
        public string Res_2 { get; set; }
        public string Renc_Id { get; set; }
        public string Equip_1 { get; set; }
        public string Equip_2 { get; set; }
        public string Equip_Id1 { get; set; }
        public string Equip_Id2 { get; set; }

    }
}

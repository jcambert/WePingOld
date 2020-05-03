using MicroS_Common.Actions;
using Microsoft.AspNetCore.Http;
using WeCommon;

namespace WePing.Actions
{
    public class NavigateToDetailRencontre: ActionWithQueryString
    {



        public string IsRetour => qs["is_retour"];
        public string Phase => qs["phase"];
        public string Res1 => qs["res_1"];
        public string Res2 => qs["res_2"];
        public string RencontreId => qs["renc_id"];
        public string Equipe1 => qs["equip_1"];
        public string Equipe2 => qs["equip_2"];
        public string EquipeId1 => qs["equip_id1"];
        public string EquipeId2 => qs["equip_id2"];

    }
}

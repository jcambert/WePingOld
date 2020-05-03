using MicroS_Common.Actions;

namespace WePing.Actions
{
    public class NavigateToClassementEquipe: ActionWithQueryString
    {
        public string NumeroClub { get; set; }
        public string Division => qs["D1"];//{ get; set; }

        public string Poule => qs["cx_poule"];// { get; set; }
    }
}

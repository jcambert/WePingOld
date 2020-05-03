using System.ComponentModel;

namespace WePing.domain
{
    public enum StatutRencontre
    {
        None,
        [Description("Gagne")]
        Gagne,
        [Description("Perdu")]
        Perdu,
        [Description("Nul")]
        Nul
    }
}

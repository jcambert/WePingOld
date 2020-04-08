using System.ComponentModel;

namespace WePing.Components
{
    public enum HiddenVisibility
    {
        [Description("d-none")]
        HideAll,
        [Description("d-none d-sm-block")]
        HideXS,
        [Description("d-sm-none d-md-block")]
        HideSM,
        [Description("d-md-none d-lg-block")]
        HideMD,
        [Description("d-lg-none d-xl-block")]
        HideLG,
        [Description("d-xl-none")]
        HideXL,
        [Description("d-block")]
        VisibleAll,
        [Description("d-block d-sm-none")]
        VisibleXS,
        [Description("d-none d-sm-block d-md-none")]
        VisibleSM,
        [Description("d-none d-md-block d-lg-none")]
        VisibleMD,
        [Description("d-none d-lg-block d-xl-none")]
        VisibleLG,
        [Description("d-none d-xl-block")]
        VisibleXL

    }


}

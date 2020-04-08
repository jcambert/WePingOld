using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
namespace WePing.Components
{
    public static class Extensions
    {
        public static string GetBootstrapClass(this string type)
           => Enum.TryParse<HiddenVisibility>(type, true, out HiddenVisibility result) ? result.GetDescription() : HiddenVisibility.VisibleAll.GetDescription();

        public static string GetDescription<T>(this T type)
            where T : Enum
            => type.GetType()
                   .GetMember(type.ToString())
                   .First()
                   .GetCustomAttribute<DescriptionAttribute>()?
                   .Description ?? string.Empty;


    }
}

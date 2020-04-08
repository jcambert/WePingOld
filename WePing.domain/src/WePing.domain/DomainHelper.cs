using MicroS_Common;
using System;
using System.ComponentModel;
using System.Linq;
namespace WePing.domain
{
    public class DomainHelper
    {
        public static string GetDescription(Type type, string propertyName)
        {
            var attr = type.GetProperty(propertyName).GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault() as DescriptionAttribute;
            return attr?.Description ?? null;
        }

        public static bool HasDisableSearchAttribute(Type type, string propertyName) => type.GetProperty(propertyName).GetCustomAttributes(typeof(DisableSearchFilterAttribute), true).FirstOrDefault() != null;
    }
}

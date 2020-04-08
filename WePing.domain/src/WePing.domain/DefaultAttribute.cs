using System;

namespace WePing.domain
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DefaultAttribute : Attribute
    {
    }

    
}

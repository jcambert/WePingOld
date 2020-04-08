using System;
using System.Threading;

namespace WePing.domain
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple =false,Inherited =true)]
    public abstract class StringConverterAttribute : Attribute
    {
        public abstract string Convert(string value);
    }
    public class LowerCaseAttribute : StringConverterAttribute
    {
        public override string Convert(string value) =>
            Thread.CurrentThread.CurrentCulture.TextInfo.ToLower(value);

    }
    public class UpperCaseAttribute : StringConverterAttribute
    {
        public override string Convert(string value)=>
            Thread.CurrentThread.CurrentCulture.TextInfo.ToUpper(value);
        
    }
}

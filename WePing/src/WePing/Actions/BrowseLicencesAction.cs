using MicroS_Common.Actions;
using System.Text.Json;
namespace WePing.Actions
{
    public class BrowseLicencesAction : IAction
    {
        //public BrowseLicences BrowseLicences { get; set; }
        public string Club { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}

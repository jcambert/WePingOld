using WePing.Models;
using WeRedux;

namespace WePing.Actions
{
    public class BrowseJoueursAction:IAction
    {
        public SearchPlayerModel Model{ get; set; }
    }
}

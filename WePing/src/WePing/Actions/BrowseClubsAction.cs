using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WePing.Models;
using WeRedux;

namespace WePing.Actions
{
    public class BrowseClubsAction:IAction
    {
        public SearchClubModel Model{ get; set; }
    }
}

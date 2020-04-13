using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeRedux;

namespace WePing.Actions
{
    public class GetLicenceAction:IAction
    {
        public string Licence { get; set; }
    }
}

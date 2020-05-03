using MicroS_Common.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeCommon;

namespace WePing.Actions
{
    public abstract class ActionWithQueryString:IAction
    {
        protected WeQueryString qs = WeQueryString.Empty;
        string _lien;
        public string Lien
        {
            get => _lien;
            set
            {
                _lien = value;
                qs += value;
            }
        }
    }
}

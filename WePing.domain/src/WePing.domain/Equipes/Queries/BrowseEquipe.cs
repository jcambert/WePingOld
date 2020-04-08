using MicroS_Common.Types;
using System;
using WePing.domain.Equipes.Dto;

namespace WePing.domain.Equipes.Queries
{
    public class BrowseEquipes : PagedQueryBase, IQuery<PagedResult<EquipeDto>>
    {
        string[] availableTypes = { "M", "F", "A", "" };
        private string _type = "A";
        public string NumClu { get; set; }

        public string Type
        {
            get { return _type; }
            set
            {
                _type = Array.IndexOf(availableTypes, value.Trim().ToUpper()) >= 0 ? value.Trim().ToUpper() : "A";
            }
        }
    }
}

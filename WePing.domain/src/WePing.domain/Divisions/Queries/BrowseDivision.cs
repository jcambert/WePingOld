using MicroS_Common.Types;
using System;
using WePing.domain.Divisions.Dto;

namespace WePing.domain.Divisions.Queries
{
    public class BrowseDivisions : PagedQueryBase, IQuery<PagedResult<DivisionDto>>
    {
        string[] availableTypes = { "E", "I" };
        private string _type = "E";
        public BrowseDivisions()
        {

        }
        public string Organisme { get; set; }

        public string Epreuve { get; set; }

        public string Type
        {
            get { return _type; }
            set
            {
                _type = Array.IndexOf(availableTypes, value.Trim().ToUpper()) >= 0 ? value.Trim().ToUpper() : "E";
            }
        }
    }
}

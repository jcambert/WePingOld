using MicroS_Common.Types;
using System;
using WePing.domain.Epreuves.Dto;

namespace WePing.domain.Epreuves.Queries
{
    public class BrowseEpreuves : PagedQueryBase, IQuery<PagedResult<EpreuveDto>>
    {
        string[] availableTypes = { "E", "I" };
        private string _type;
        public string Organisme { get; set; }
        public string Type
        {
            get { return _type; }
            set
            {
                _type = Array.IndexOf(availableTypes, value?.Trim()?.ToUpper()) >= 0 ? value.Trim().ToUpper() : "E";

            }
        }
    }
}

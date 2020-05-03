using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WePing.domain.JoueurDetails.Dto;
using WePing.domain.Licences.Dto;

namespace WePing.Data
{
    public class MyProfile
    {
        public string NumeroLicence { get; set; }
        public List<Favorite> Favorites { get; set; }
        [JsonIgnore]
        public LicenceDto Licence { get; set; }
        [JsonIgnore]
        public JoueurDetailDto Joueur { get; set; }
    }
    public class Favorite
    {

    }
}

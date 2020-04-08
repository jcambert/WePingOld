using System.ComponentModel.DataAnnotations;

namespace WePing.Models
{
    public class SearchClubModel
    {
        [Display(Name = "DEPARTEMENT")]
        public string Dep { get; set; }
        [Display(Name = "CODE")]
        public string Code { get; set; }
        [Display(Name = "VILLE")]
        public string Ville { get; set; }
        [Display(Name = "NUMERO")]
        public string Numero { get; set; }
    }
}

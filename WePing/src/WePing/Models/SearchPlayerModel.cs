using System.ComponentModel.DataAnnotations;
using WeStrap;
namespace WePing.Models
{
    public class SearchPlayerModel
    {
        [UpperCase]
        [Required]
        [Display(Name = "FIRSTNAME")]
        public string Nom { get; set; }
        [TitleCase]
        [Display(Name = "LASTNAME")]
        public string Prenom { get; set; }
        [Display(Name = "LICENCE")]
        public string Licence { get; set; }
    }
}

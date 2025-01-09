using System.ComponentModel.DataAnnotations;
using CV_ASP.NET.Models.ViewModels;

namespace CV_ASP.NET.Models.ViewModels
{
    public class SkickaMeddelandeViewModel
    {

        public string TillAnvandareId { get; set; }
       
        [Required(ErrorMessage = "AnonymAnvandare är obligatorisk.")]
        public string AnonymAnvandare { get; set; }
        public string Innehall { get; set; }

        public string Meddelande { get; set; }
    }

}

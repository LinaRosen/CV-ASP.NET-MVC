using System.ComponentModel.DataAnnotations;
using CV_ASP.NET.Models.ViewModels;


namespace CV_ASP.NET.Models.ViewModels
{
    public class SkickaMeddelandeViewModel
    {
        public List<Meddelande> Meddelanden { get; set; }
        public string Avsandare { get; set; }
        public int Mid { get; set; }
        public string TillAnvandareId { get; set; }

        public string FranAnvandareId { get; set; }

        // AnonymAnvandare ska endast vara obligatorisk om användaren inte är inloggad
        [Required(ErrorMessage = "AnonymAnvandare är obligatorisk.",
                  AllowEmptyStrings = false
                  )]


        public string? AnonymAnvandare { get; set; }

        [Required(ErrorMessage = "Meddelandeinnehåll är obligatoriskt.")]
        public string Innehall { get; set; }

        // För att hålla reda på om användaren är inloggad
        public bool IsAuthenticated { get; set; }
    }
}




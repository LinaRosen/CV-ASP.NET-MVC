using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CV_ASP.NET.Models.ViewModels
{
    public class RedigeraCvViewModel
    {
        
        public string? Profilbild {  get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Du måste lägga in en bild till ditt cv.")]
        public IFormFile? BildFil { get; set; }

        [RegularExpression(@"^[\p{L}\d]*$", ErrorMessage = "Vänligen ange endast bokstäver och siffror i beskrivningen.")]
        [Required(ErrorMessage = "Vänligen skriv en beskrivning av ditt cv.")]
        public string? Beskrivning { get; set; }
    }
}

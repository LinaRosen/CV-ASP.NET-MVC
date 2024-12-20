using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CV_ASP.NET.Models
{
    public class Kompetenser
    {
        [Key]
        public int Kid { get; set; }

        [Required(ErrorMessage = "Du måste ange ett namn")]
        [DisplayName("Namn på kompetens")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "Du får endast ange bokstäver!")]
        public string KompetensNamn { get; set; }

        [Required(ErrorMessage = "Du måste ange en beskrivning")]
        [DisplayName("Beskrivning av kompetens")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "Du får endast ange bokstäver!")]
        public string Beskrivning { get; set; }

        public virtual IEnumerable<CV_kompetenser> CV_kompetenser { get; set; } = new List<CV_kompetenser>();


    }
}

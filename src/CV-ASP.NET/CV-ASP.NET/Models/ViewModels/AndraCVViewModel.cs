using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace CV_ASP.NET.Models
{
    public class AndraCVViewModel
    {
        [NotMapped]


        [DisplayName("Beskrivning")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "Du får endast ange bokstäver!")]
        public string Beskrivning { get; set; }
    }
}

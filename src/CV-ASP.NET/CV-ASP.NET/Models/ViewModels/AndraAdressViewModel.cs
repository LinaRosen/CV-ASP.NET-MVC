using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CV_ASP.NET.Models
{
    public class AndraAdressViewModel

    {

        [Required(ErrorMessage = "Du måste ange ett gatunamn")]
        [RegularExpression(@"^[A-Za-zåäöÅÄÖ\s\-\.]+ \d+$", ErrorMessage = "Ange ett giltigt gatunamn följt av ett nummer!")]
        [DisplayName("Gatunamn & husnummer")]
        public string Gatunamn { get; set; }

        [Required(ErrorMessage = "Du måste ange en stad")]
        [RegularExpression(@"^[A-Za-zåäöÅÄÖ\s\-\.]+$", ErrorMessage = "Ange ett giltigt stadsnamn!")]
        [DisplayName("Stad")]
        public string Stad { get; set; }

        [Required(ErrorMessage = "Du måste ange ett postnummer")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Postnummer kan endast innehålla siffror")]
        [DisplayName("Postnummer")]
        public int Postnummer { get; set; }
    }
}

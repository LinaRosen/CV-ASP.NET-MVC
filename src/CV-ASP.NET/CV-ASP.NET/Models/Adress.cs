using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CV_ASP.NET.Models
{
    public class Adress
    {
        [Key]
        public int Aid {  get; set; }

        [Required(ErrorMessage = "Du måste ange ett gatunamn")]
        [RegularExpression(@"^[A-Za-zåäöÅÄÖ\s\-\.]+ \d+$", ErrorMessage = "Ange ett giltigt gatunamn följt av ett nummer!")]
        //[DisplayName("Gatunamn & husnummer")]
        public string Gatunamn { get; set; }

        [Required(ErrorMessage = "Du måste ange en stad")]
        [RegularExpression(@"^[A-Za-zåäöÅÄÖ\s\-\.]+$", ErrorMessage = "Ange ett giltigt stadsnamn!")]
        //[DisplayName("Stad")]
        public string Stad { get; set; }

        [Required(ErrorMessage = "Du måste ange ett postnummer")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Postnummer kan endast innehålla siffror")]
        //[DisplayName("Postnummer")]
        public string Postnummer { get; set; }

        [Required]
        public string Anvid { get; set; }

        [ForeignKey(nameof(Anvid))]
        public virtual Anvandare Anvandare { get; set; }

    }
}

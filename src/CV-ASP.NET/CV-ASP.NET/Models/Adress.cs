using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CV_ASP.NET.Models
{
    public class Adress
    {
        [Key]
        public int Aid {  get; set; }

        [Required(ErrorMessage = "Du måste ange ett gatunamn")]
        [RegularExpression(@"^[\p{L}\s]+\s\d+$", ErrorMessage = "Ange ett giltigt gatunamn följt av ett nummer!")]

        public string Gatunamn { get; set; }

        [Required(ErrorMessage = "Du måste ange en stad")]
        [RegularExpression(@"^[\p{L}\s-]+$", ErrorMessage = "Ange ett giltigt stadsnamn!")]

        public string Stad { get; set; }

        [Required(ErrorMessage = "Du måste ange ett postnummer")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Postnummer kan endast innehålla siffror")]

        public int Postnummer { get; set; }

        public string Anvid { get; set; }


        [ForeignKey(nameof(Anvid))]
        public virtual Anvandare? anvandare { get; set; }

    }
}

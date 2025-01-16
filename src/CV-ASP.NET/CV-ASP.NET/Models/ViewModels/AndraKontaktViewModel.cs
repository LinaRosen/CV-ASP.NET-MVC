using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CV_ASP.NET.Models
{
    public class AndraKontaktViewModel
    {
        [Required(ErrorMessage = "Användarnamn är obligatoriskt.")]
        [DisplayName("Användarnamn")]
        [RegularExpression(@"^[a-zA-Z0-9._-]{3,20}$", ErrorMessage = "Fel format på användarnamnet")]
        public string Anvandarnamn { get; set; }

        [Required(ErrorMessage = "Förnamn är obligatoriskt.")]
        [DisplayName("Förnamn")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "Du får endast ange bokstäver!")]
        public string Fornamn { get; set; }

        [Required(ErrorMessage = "Efternamn är obligatoriskt.")]
        [DisplayName("Efternamn")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "Du får endast ange bokstäver!")]
        public string Efternamn { get; set; }

        [Required(ErrorMessage = "E-postadress är obligatorisk.")]
        [DisplayName("E-postadress")]
        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "Ogiltig e-postadress. Ange en giltig e-postadress.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefonnummer är obligatoriskt.")]
        [DisplayName("Telefonnummer")]
        [RegularExpression(@"^\+?\d{1,4}[-.\s]?\(?\d{1,3}\)?[-.\s]?\d{3,4}[-.\s]?\d{4}$", ErrorMessage = "Ogiltigt telefonnummer. Ange ett giltigt telefonnummer.")]
        public string Telefonnummer { get; set; }

        public bool PrivatProfil { get; set; }

        [Required(ErrorMessage = "Du måste ange ett gatunamn")]
        [RegularExpression(@"^[A-Za-zåäöÅÄÖ\s\-\.]+ \d+$", ErrorMessage = "Ange ett giltigt gatunamn följt av ett nummer!")]
        public string Gatunamn { get; set; }

        [Required(ErrorMessage = "Du måste ange en stad")]
        [RegularExpression(@"^[A-Za-zåäöÅÄÖ\s\-\.]+$", ErrorMessage = "Ange ett giltigt stadsnamn!")]
        public string Stad { get; set; }

        [Required(ErrorMessage = "Du måste ange ett postnummer")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Postnummer kan endast innehålla siffror")]
        public string Postnummer { get; set; }
    }
}

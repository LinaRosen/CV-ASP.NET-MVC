 using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CV_ASP.NET.Models
{
    public class AndraKontaktViewModel
    {
        [DisplayName("Användarnamn")]
        [RegularExpression(@"^[a-zA-Z0-9._-]{3,20}$", ErrorMessage = "Fel format på användarnamnet")]
        public string Anvandarnamn { get; set; }

        [DisplayName("Förnamn")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "Du får endast ange bokstäver!")]
        public string Fornamn { get; set; }

        [DisplayName("Efternamn")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "Du får endast ange bokstäver!")]
        public string Efternamn { get; set; }

        [DisplayName("E-postadress")]
        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "Ogiltig e-postadress. Ange en giltig e-postadress.")]
        public string Email { get; set; }

        [DisplayName("Telefonnummer")]
        [RegularExpression(@"^\+?\d{1,4}[-.\s]?\(?\d{1,3}\)?[-.\s]?\d{3,4}[-.\s]?\d{4}$", ErrorMessage = "Ogiltigt telefonnummer. Ange ett giltigt telefonnummer.")]
        public string Telefonnummer { get; set; }

        [DisplayName("Lösenord")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Lösenordet måste vara minst {2} tecken långt.", MinimumLength = 6)]
        public string Losenord { get; set; }

        [DisplayName("Bekräfta lösenord")]
        [DataType(DataType.Password)]
        [Compare("Losenord", ErrorMessage = "Lösenorden matchar inte.")]
        public string BekraftaLosenord { get; set; }

        public bool PrivatProfil {  get; set; } 


    }
}

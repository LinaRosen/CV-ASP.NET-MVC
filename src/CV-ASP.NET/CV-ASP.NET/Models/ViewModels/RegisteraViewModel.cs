using System.ComponentModel.DataAnnotations;

namespace CV_ASP.NET.Models.ViewModels
{

        public class RegistreraViewModel
        {

            [Required(ErrorMessage = "Du måste ange ett användarnamn")]
            [RegularExpression(@"^[a-zA-Z0-9._-]{3,20}$", ErrorMessage = "Fel format på användarnamnet")]
            public string Anvandarnamn { get; set; }

            [Required(ErrorMessage = "Du måste ange ett förnamn")]
            [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Du får endast ange bokstäver!")]

            public string Fornamn { get; set; }

            [Required(ErrorMessage = "Du måste ange ett efternamn")]
            [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Du får endast ange bokstäver!")]

            public string Efternamn { get; set; }

            [Required(ErrorMessage = "Du måste ange en e-postadress")]
            [EmailAddress(ErrorMessage = "Ogiltig e-postadress")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Du måste ange ett lösenord")]
            [DataType(DataType.Password)]
            public string Losenord { get; set; }

            [Required(ErrorMessage = "Du måste bekräfta ditt lösenord")]
            [Compare("Losenord", ErrorMessage = "Lösenorden matchar inte")]
            [DataType(DataType.Password)]
            public string BekraftaLosenord { get; set; } 

            public bool PrivatProfil { get; set; }

            public IFormFile? Profilbild { get; set; }

        
            [Required(ErrorMessage = "Telefonnummer är obligatoriskt")]
            [RegularExpression(@"^\d{10}$", ErrorMessage = "Telefonnummer måste vara 10 siffror")]
            public string Telefonnummer { get; set; }
        
            public Adress Adress { get; set; } = new Adress();
    }
}




using System.ComponentModel.DataAnnotations;

namespace CV_ASP.NET.Models
{
    public class LoggaInViewModel
    {
        [Required(ErrorMessage = "Du måste ange ett användarnamn")]
        [RegularExpression(@"^[a-zA-Z0-9._-]{3,20}$", ErrorMessage = "Fel format på användarnamnet")]
        public string Anvandarnamn { get; set; }

        [Required(ErrorMessage = "Du måste ange ett lösenord")]
        [DataType(DataType.Password)]
        public string Losenord { get; set; }
    }
}

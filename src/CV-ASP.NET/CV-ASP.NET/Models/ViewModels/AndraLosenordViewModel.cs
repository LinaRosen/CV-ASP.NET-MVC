using System.ComponentModel.DataAnnotations;

namespace CV_ASP.NET.Models.ViewModels
{
    public class AndraLosenordViewModel
    {
        [Required(ErrorMessage = "Du måste ange ditt nuvarande lösenord")]
        public string NuvarandeLosenord { get; set; }

        [Required(ErrorMessage = "Du måste ange ett nytt lösenord")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Lösenordet måste vara minst 6 tecken långt")]
        public string NyttLosenord { get; set; }

        [Required(ErrorMessage = "Du måste bekräfta ditt nya lösenord")]
        [Compare("NyttLosenord", ErrorMessage = "Lösenorden matchar inte")]
        public string BekraftaNyttLosenord { get; set; }
    }
}


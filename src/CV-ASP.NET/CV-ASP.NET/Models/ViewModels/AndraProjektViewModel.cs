using System.ComponentModel.DataAnnotations;

namespace CV_ASP.NET.Models.ViewModels
{
    public class AndraProjekt
    {
        [Required(ErrorMessage = "Var god ange en titel.")]
        public string? Titel { get; set; }

        [Required(ErrorMessage = "Var god skriv en beskrivning av projektet.")]
        public string? Beskrivning { get; set; }
    }
}

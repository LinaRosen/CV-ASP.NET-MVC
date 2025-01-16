using System.ComponentModel.DataAnnotations;

namespace CV_ASP.NET.Models.ViewModels
{
    public class AndraProjekt
    {
        [Required(ErrorMessage = "Var god ange en titel.")]
        [RegularExpression(@"^[a-zA-Z0-9\s,\._-]+$", ErrorMessage = "Titeln får endast innehålla bokstäver, siffror, mellanslag, komma, punkt, bindestreck och understreck.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Titeln måste vara mellan 3 och 100 tecken lång.")]
        public string? Titel { get; set; }

        [Required(ErrorMessage = "Du måste ange en beskrivning på projektet.")]
        [RegularExpression(@"^[a-zA-Z0-9\s,\._-]+$", ErrorMessage = "Beskrivningen får endast innehålla bokstäver, siffror, mellanslag, komma, punkt, bindestreck och understreck.")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Beskrivningen måste vara mellan 10 och 500 tecken lång.")]
        public string? Beskrivning { get; set; }
    }
}

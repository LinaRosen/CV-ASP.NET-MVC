using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace CV_ASP.NET.Models
{
    public class Projekt
    {
        [Key]
        public int Pid { get; set; }

        [Required(ErrorMessage = "Du måste ange ett namn på projektet")]
        [RegularExpression(@"^[a-zA-Z0-9._-]{3,20}$", ErrorMessage = "Får endast innehålla bokstäver och siffror")]
        public string? Namn { get; set; }

        [Required(ErrorMessage = "Du måste ange en beskrivning på projektet")]
        [RegularExpression(@"^[a-zA-Z0-9\s,\._-]{3,20}$", ErrorMessage = "Får endast innehålla bokstäver, siffror, mellanslag, komma, punkt och bindestreck.")]
        public string? Beskrivning { get; set; }
        public string? SkapadAv { get; set; }
        public DateOnly DatumSkapad { get; set; } = DateOnly.FromDateTime(DateTime.Now);


        [ForeignKey(nameof(SkapadAv))]
        [XmlIgnore]
        public virtual Anvandare? Anvandare { get; set; }

        [XmlIgnore]
        public virtual IEnumerable<AnvProjekt> AnvProjekt { get; set; } = new List<AnvProjekt>();


    }
}

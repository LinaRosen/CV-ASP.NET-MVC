using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace CV_ASP.NET.Models
{
    public class CV
    {
        public int Cvid {  get; set; } 
        public string? Profilbild { get; set; }

        [Required(ErrorMessage = "Var god ange en beskrivning!")]
        [DisplayName("Beskrivning")]
        public string Beskrivning { get; set; }

        [NotMapped]
        [DisplayName("Bild")]
        [XmlIgnore]
        public IFormFile? Bildfil { get; set; }
        public string? AnvandarNamn { get; set; }
        public int? AntalBesokare { get; set; } = 0;

        [ForeignKey(nameof(AnvandarNamn))]
        [XmlIgnore]
        public virtual Anvandare? anvandare { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Serialization;

namespace CV_ASP.NET.Models
{
    public class Erfarenhet
    {
        [Key]
        public int Eid { get; set; }

        [Required(ErrorMessage = "Du måste ange en titel")]
        [DisplayName("Titel")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "Du får endast ange bokstäver!")]
        public string Titel { get; set; }

        [Required(ErrorMessage = "Du måste ange en beskrivning")]
        [DisplayName("Beskrivning av erfarenhet")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "Du får endast ange bokstäver!")]
        public string Beskrivning { get; set; }

        [Required(ErrorMessage = "Du måste ange en arbetsgivare")]
        [DisplayName("Arbetsgivare")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "Du får endast ange bokstäver!")]
        public string Arbetsgivare { get; set; }

        [XmlIgnore]
        public virtual IEnumerable<CV_Erfarenhet> cv_Erfarenhet { get; set; } = new List<CV_Erfarenhet>();
    }
}

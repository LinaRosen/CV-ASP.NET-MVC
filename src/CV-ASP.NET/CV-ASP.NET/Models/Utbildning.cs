using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Serialization;

namespace CV_ASP.NET.Models
{
    public class Utbildning
    {
        public int Uid { get; set; }

        [Required(ErrorMessage = "Du måste ange ett namn")]
        [DisplayName("Namn på instutition")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "Du får endast ange bokstäver!")]
        public string Instutition { get; set; }

        [Required(ErrorMessage = "Du måste ange kurs/program")]
        [DisplayName("Kurs/program")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "Du får endast ange bokstäver!")]
        public string Kurs_program { get; set; }

      
        
        [Required(ErrorMessage = "Du måste ange en beskrivning")]
        [DisplayName("Beskrivning av kurs/program")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "Du får endast ange bokstäver!")]
        public string Beskrivning { get; set; }

        [XmlIgnore]
        public virtual IEnumerable<CV_Utbildning> cv_Utbildning { get; set; }  = new List<CV_Utbildning>();
    }
}

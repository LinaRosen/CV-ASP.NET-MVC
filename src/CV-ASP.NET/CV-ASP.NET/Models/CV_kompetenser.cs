using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace CV_ASP.NET.Models
{
    public class CV_kompetenser
    {
        public int Kid { get; set; }
        public int Cvid { get; set; }
        
        [Required(ErrorMessage = "Du måste ange ett namn")]
        [DisplayName("Namn på kompetens")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "Du får endast ange bokstäver!")]
        public string KompetensNamn { get; set; }

        [ForeignKey(nameof(Cvid))]
        [XmlIgnore]
        public virtual CV? CV { get; set; }
        
        [ForeignKey(nameof(Kid))]
        [XmlIgnore]
        public virtual Kompetenser? Kompetenser { get; set; }
    }
}

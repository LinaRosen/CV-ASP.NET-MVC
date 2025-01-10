using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace CV_ASP.NET.Models
{
    
    public class CV_Erfarenhet
    {
        
        public int Eid { get; set; }
        
        public int Cvid { get; set; }

        [Required(ErrorMessage = "Du måste ange startdatum")]
        [DisplayName("Startdatum")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "Fel format. Datum ska vara i formatet yyyy-MM-dd.")]
        public DateOnly Startdatum { get; set; }

       
        [DisplayName("Slutdatum")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "Fel format. Datum ska vara i formatet yyyy-MM-dd.")]
        public DateOnly? Slutdatum { get; set; }

        [ForeignKey(nameof(Eid))]
        [XmlIgnore]
        public virtual Erfarenhet? erfarenhet { get; set; }
        
        [ForeignKey(nameof(Cvid))]
        [XmlIgnore]
        public virtual CV? cv { get; set; }
    }
}

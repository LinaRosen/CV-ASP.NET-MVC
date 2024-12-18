using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace CV_ASP.NET.Models
{
    public class CV_Utbildning
    {
        public int Uid { get; set; }
        public int CVid { get; set; }

        [Required(ErrorMessage = "Du måste ange startdatum")]
        [DisplayName("Startdatum")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "Fel format. Datum ska vara i formatet yyyy-MM-dd.")]
        public DateOnly Startdatum { get; set; }

        [Required(ErrorMessage = "Du måste ange slutdatum")]
        [DisplayName("Slutdatum")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "Fel format. Datum ska vara i formatet yyyy-MM-dd.")]
        public DateOnly? Slutdatum { get; set; }

        [ForeignKey(nameof(Uid))]
        [XmlIgnore]
        public virtual Utbildning? utbildning{ get; set; }

        [ForeignKey(nameof(CVid))]
        [XmlIgnore]
        public virtual CV? cv { get; set; }
    }
}

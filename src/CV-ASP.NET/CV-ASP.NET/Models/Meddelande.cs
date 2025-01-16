using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace CV_ASP.NET.Models
{
    public class Meddelande
    {
        [Key]
        public int Mid { get; set; }


        [Required(ErrorMessage = "Innehållet är obligatoriskt.")]
        [StringLength(1000, ErrorMessage = "Innehållet får vara högst 1000 tecken långt.")]
        public string? Innehall { get; set; }
        public bool Last { get; set; }
        public string? FranAnvandareId { get; set; }
        public string? TillAnvandareId { get; set; }




        [Required(ErrorMessage = "Anonym användare är obligatoriskt.")]
        public string? AnonymAnvandare { get; set; }

        [ForeignKey(nameof(FranAnvandareId))]
        [XmlIgnore]
        public virtual Anvandare? Frananvandare { get; set; }

        [ForeignKey(nameof(TillAnvandareId))]
        [XmlIgnore]
        public virtual Anvandare? Tillanvandare { get; set; }
        
    }
}

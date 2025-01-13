using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace CV_ASP.NET.Models
{
    public class Meddelande
    {
        [Key]
        public int Mid { get; set; }
        public string? Innehall { get; set; }
        public bool Last { get; set; }
        public string? FranAnvandareId { get; set; }
        public string? TillAnvandareId { get; set; }
        public int OlastaMeddelandenCount { get; set; }

        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "Du får endast ange bokstäver!")]
        [Required(AllowEmptyStrings = true)]
        public string? AnonymAnvandare { get; set; }

        [ForeignKey(nameof(FranAnvandareId))]
        [XmlIgnore]
        public virtual Anvandare? Frananvandare { get; set; }

        [ForeignKey(nameof(TillAnvandareId))]
        [XmlIgnore]
        public virtual Anvandare? Tillanvandare { get; set; }
        public string Avsandare { get; set; }  // Lägg till avsändare
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace CV_ASP.NET.Models
{
    public class Meddelande
    {
        public int Mid { get; set; }
        public string? Innehall { get; set; }
        public bool? Last {  get; set; }
        public string? FranAnvandare { get; set; }
        public string? TillAnvandare { get; set; }

        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "Du får endast ange bokstäver!")]
        public string? AnonymAnvandare { get; set; }

        [ForeignKey(nameof(FranAnvandare))]
        [XmlIgnore]
        public virtual Anvandare? Frananvandare { get; set; }

        [ForeignKey(nameof(TillAnvandare))]
        [XmlIgnore]
        public virtual Anvandare? Tillanvandare { get; set; }
    }
}

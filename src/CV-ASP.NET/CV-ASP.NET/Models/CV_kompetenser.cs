using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace CV_ASP.NET.Models
{
    public class CV_kompetenser
    {
        public int Kid { get; set; }
        public int Cvid { get; set; }
        [ForeignKey(nameof(Cvid))]
        [XmlIgnore]
        public virtual CV? CV { get; set; }
        
        [ForeignKey(nameof(Kid))]
        [XmlIgnore]
        public virtual Kompetenser? Kompetenser { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace CV_ASP.NET.Models
{
    public class AnvProjekt
    {
        public string Anvid { get; set; }
        public int Pid { get; set; }

        [ForeignKey(nameof(Anvid))]
        [XmlIgnore]
        public virtual Anvandare? Anvandare { get; set; }

        [ForeignKey(nameof(Pid))]
        [XmlIgnore]
        public virtual Projekt? Projekt { get; set; }

    }
}

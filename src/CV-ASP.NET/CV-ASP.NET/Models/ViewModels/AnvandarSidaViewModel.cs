using System.ComponentModel.DataAnnotations.Schema;

namespace CV_ASP.NET.Models
{
    public class AnvandarSidaViewModel
    {
        public virtual AnvandarSidaViewModel anvandarSida { get; set; }
        public virtual CV CV { get; set; }
        public string InloggadAnvandare { get; set; }

        public virtual Adress adress { get; set; }

        [NotMapped]
        public IFormFile? Bildfil { get; set; }
    }
}

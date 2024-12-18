using System.ComponentModel.DataAnnotations.Schema;

namespace CV_ASP.NET.Models
{
    public class AnvandarSida
    {
        public AnvandarSida anvandarSida { get; set; }
        public CV CV { get; set; }
        public string InloggadAnvandare { get; set; }

        public Adress adress { get; set; }

        [NotMapped]
        public IFormFile? Bildfil { get; set; }
    }
}

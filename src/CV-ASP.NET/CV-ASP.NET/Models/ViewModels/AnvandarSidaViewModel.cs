using System.ComponentModel.DataAnnotations.Schema;

namespace CV_ASP.NET.Models
{
    public class AnvandarSidaViewModel
    {
        public Anvandare? anvandare { get; set; }
        public  CV? CV { get; set; }
        public string? InloggadAnvandare { get; set; }

        public Adress? adress { get; set; }

        public string? epost { get; set; }
       


        [NotMapped]
        public IFormFile? Bildfil { get; set; }
    }
}

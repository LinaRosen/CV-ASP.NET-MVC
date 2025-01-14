using System.ComponentModel.DataAnnotations.Schema;

namespace CV_ASP.NET.Models
{
    public class AnvandarSidaViewModel
    {
        public Anvandare? anvandare { get; set; }
        public  CV? CV { get; set; }
        public ICollection<AnvProjekt> Projekt { get; set; }
        public string? InloggadAnvandare { get; set; }

        public string? Gatunamn { get; set; }
        public string? Stad { get; set; }
        public string? Postnummer { get; set; }

        public string? epost { get; set; }
       


        [NotMapped]
        public IFormFile? Bildfil { get; set; }
    }
}

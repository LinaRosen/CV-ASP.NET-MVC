using System.ComponentModel.DataAnnotations.Schema;

namespace CV_ASP.NET.Models
{
    public class MeddelandeViewModel
    {
        public int Mid { get; set; } // ID för meddelandet
        public string Innehall { get; set; } = "Inget innehåll"; // Standardvärde om tomt
        public bool Last { get; set; } // Om meddelandet har lästs
        public string? TillAnvandareId { get; set; }
        public string? FranAnvandareId { get; set; }
        public string? AnonymAnvandare { get; set; }

        public string Avsandare { get; set; }
        public List<Meddelande> Meddelanden { get; set; }
        public int OlastaMeddelandenCount { get; set; }// Antal olästa meddelanden.
                                                       //public Meddelande med { get; set; }


        public Anvandare anv { get; set; }

    }
}







namespace CV_ASP.NET.Models
{
    public class MeddelandeSidaViewModel
    {
        public List<MeddelandeViewModel> Meddelanden { get; set; } = new List<MeddelandeViewModel>();
        public string InloggadAnvandare { get; set; }  // Ny egenskap för att lagra användarnamn
        // Egenskaper för meddelandens data
        public int Mid { get; set; } // ID för meddelandet
        public string Innehall { get; set; } = "Inget innehåll"; // Standardvärde om tomt
        public bool? Last { get; set; } // Om meddelandet har lästs
        public string? TillAnvandareId { get; set; }
        public string? FranAnvandareId { get; set; }
        public DateTime SkapadDatum { get; set; } // Datum för skapande
        public string NyttMeddelandeText { get; set; } // För att skicka ett nytt meddelande
        public int OlastaMeddelandenCount { get; set; }


    }
}


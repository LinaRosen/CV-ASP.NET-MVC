namespace CV_ASP.NET.Models.ViewModels
{
    public class ProjektVy
    {
        public int Id { get; set; }
        public string Namn { get; set; }
        public string Beskrivning { get; set; }
        public DateOnly DatumSkapad { get; set; }
        public List<string> Medlemmar { get; set; }
        public bool SkapadAvInloggadAnv { get; set; }
        public bool ArMedlem { get; set; }

    }
}

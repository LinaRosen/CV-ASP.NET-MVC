namespace CV_ASP.NET.Models.ViewModels
{
    public class Match
    {
        public Anvandare Anvandare { get; set; }
        public IEnumerable<Erfarenhet> Erfarenheter { get; set; }

        public IEnumerable<Kompetenser> Kompetenser { get; set; }
        public CV Cv { get; set; }
    }
}

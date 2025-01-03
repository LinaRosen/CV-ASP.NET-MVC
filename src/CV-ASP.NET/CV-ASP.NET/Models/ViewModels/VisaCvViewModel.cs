namespace CV_ASP.NET.Models.ViewModels
{
    public class VisaCvViewModel
    {
        public CV Cv { get; set; }
        public Anvandare anvandare { get; set; }
        public IEnumerable<Utbildning> utbildning { get; set; }
        public IEnumerable<Erfarenhet> erfarenhet { get; set; }
        public IEnumerable<Kompetenser> kompetenser { get; set; }
        public IEnumerable<Projekt> projekt { get; set; }
        
    }

}

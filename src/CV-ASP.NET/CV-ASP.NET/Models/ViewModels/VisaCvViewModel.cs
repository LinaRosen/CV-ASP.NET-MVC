namespace CV_ASP.NET.Models.ViewModels
{
    public class VisaCvViewModel
    {
        public CV Cv { get; set; }
        public Anvandare anvandare { get; set; }
        public IEnumerable<UtbildningViewModel> utbildningar { get; set; }
        public IEnumerable<ErfarenhetViewModel> erfarenheter { get; set; }
        public IEnumerable<KompetensViewModel> kompetenser { get; set; }
        public IEnumerable<Projekt> projekt { get; set; }
        public string TillAnvandareId { get; set; }

    }

}

namespace CV_ASP.NET.Models.ViewModels
{
    public class StartsidaViewModel
    {
        public IEnumerable<Anvandare> Anvandare { get; set; }
        public IEnumerable<Projekt> Projekt { get; set; }
        public IEnumerable<CV> Cv { get; set; }

        
    }
}

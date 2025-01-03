namespace CV_ASP.NET.Models.ViewModels
{
    public class SokViewModel
    {
        public string Namn { get; set; }
        public IEnumerable<Anvandare> Anvandare { get; set; }
    }

}

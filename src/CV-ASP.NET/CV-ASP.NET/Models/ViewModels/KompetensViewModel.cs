namespace CV_ASP.NET.Models.ViewModels
{
    public class KompetensViewModel
    {
        public string Beskrivning {  get; set; }
        public Kompetenser Kompetenser { get; set; }
        public CV_kompetenser cV_Kompetenser { get; set; }

        public string KompetensNamn { get; set; }

    }
}

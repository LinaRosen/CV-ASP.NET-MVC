namespace CV_ASP.NET.Models.ViewModels
{
    public class UtbildningViewModel
    {
        public Utbildning utbildning {  get; set; }
        public CV_Utbildning cV_Utbildning { get; set; }
        public string Instutition { get; set; }
        public string Kurs_program { get; set; }
        public string Beskrivning { get; set; }
        public DateOnly? StartDatum { get; set; }
        public DateOnly? SlutDatum { get; set; }

    }
}

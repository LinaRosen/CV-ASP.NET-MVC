namespace CV_ASP.NET.Models
{
    public class VisaMeddelande
    {
        public Meddelande meddelande {  get; set; }
        public string? AnonymAnvandare { get; set; }
        public string? FranAnvandarNamn { get; set; }
        public bool? TabortMeddelande { get; set; } = false;

    }
}

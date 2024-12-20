namespace CV_ASP.NET.Models
{
    public class VisaMeddelandeViewModel
    {
        public virtual Meddelande meddelande {  get; set; }
        public string? AnonymAnvandare { get; set; }
        public string? FranAnvandarNamn { get; set; }
        public bool? TabortMeddelande { get; set; } = false;

    }
}

namespace CV_ASP.NET.Models.ViewModels
{
    public class VisaMeddelande
    {
       
            public Meddelande Meddelande { get; set; }
            public string FranAnonym { get; set; }
            public string FramAnv { get; set; }
            public bool TaBortMed { get; set; } = false;
        
    }
}

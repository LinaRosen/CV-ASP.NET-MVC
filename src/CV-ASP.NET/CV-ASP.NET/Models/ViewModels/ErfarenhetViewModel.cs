namespace CV_ASP.NET.Models.ViewModels
{
    public class ErfarenhetViewModel
    {
          Erfarenhet erfarenhet {  get; set; }    
         CV_Erfarenhet cV_Erfarenhet { get; set;}

        public string Titel { get; set; }
        public string Beskrivning { get; set; }
        public string Arbetsgivare { get; set; }
        public DateOnly? StartDatum { get; set; }

        public DateOnly? Slutdatum { get; set; }
        public int Eid { get; set; }
        

    }
}

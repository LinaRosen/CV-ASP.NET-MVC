using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Xml.Serialization;

namespace CV_ASP.NET.Models.ViewModels
{
    public class SkapaCvViewModel

    {
        public CV cv {  get; set; }
        public Erfarenhet erfarenhet { get; set; }
        public Kompetenser kompetenser { get; set; }
        public Utbildning utbildning { get; set; }
        public CV_Erfarenhet cvErfarenhet { get; set; }
        public CV_kompetenser cvKompetenser { get; set; }
        public CV_Utbildning cvUtbildning { get; set; } 

        
           
    }
}

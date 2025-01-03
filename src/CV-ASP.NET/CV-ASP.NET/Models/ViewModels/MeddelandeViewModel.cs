using System.ComponentModel.DataAnnotations.Schema;

namespace CV_ASP.NET.Models
{
    public class MeddelandeViewModel
    {


        // Lista av Meddelande som ska visas på sidan
        public List<Meddelande> Meddelanden { get; set; } = new List<Meddelande>();

        // Eventuellt andra egenskaper, t.ex. för att skapa ett nytt meddelande
        public string NyttMeddelandeText { get; set; }

    }
}



using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace CV_ASP.NET.Models
{
    public class Anvandare : IdentityUser
    {
        
        //public string? Anvid { get; set; } //Tror inte vi behöver denna då identityUser skapar en automatiskt

        [Required(ErrorMessage = "Du måste ange ett användarnamn")]
        [RegularExpression(@"^[a-zA-Z0-9._-]{3,20}$", ErrorMessage = "Fel format på användarnamnet")]
        public string Anvandarnamn { get; set; }


        [Required(ErrorMessage = "Du måste ange ett förnamn")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "Du får endast ange bokstäver!")]
        public string Fornamn { get; set; }

        [Required(ErrorMessage = "Du måste ange ett efternamn")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "Du får endast ange bokstäver!")]
        public string Efternamn { get; set; }

        public bool PrivatProfil {  get; set; } = false;

        public bool Aktiverad { get; set; } = false;
        public bool ListadStartsida { get; set; } = false;

        [Required(ErrorMessage = "Du måste ange ett gatunamn")]
        [RegularExpression(@"^[A-Za-zåäöÅÄÖ\s\-\.]+ \d+$", ErrorMessage = "Ange ett giltigt gatunamn följt av ett nummer!")]
        public string Gatunamn { get; set; }

        [Required(ErrorMessage = "Du måste ange en stad")]
        [RegularExpression(@"^[A-Za-zåäöÅÄÖ\s\-\.]+$", ErrorMessage = "Ange ett giltigt stadsnamn!")]
        public string Stad { get; set; }

        [Required(ErrorMessage = "Du måste ange ett postnummer")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Postnummer kan endast innehålla siffror")]
        public string Postnummer { get; set; }

        [XmlIgnore]
        public virtual CV? CV { get; set; }
        
        [XmlIgnore]
        public virtual ICollection<Meddelande> skickatMeddelande { get; set; } = new List<Meddelande>();

        [XmlIgnore]
        public virtual ICollection<Meddelande> TagitEmotMeddelande { get; set; } = new List<Meddelande>();
       
    }

}

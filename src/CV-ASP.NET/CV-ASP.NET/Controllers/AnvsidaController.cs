using CV_ASP.NET.DataContext;
using CV_ASP.NET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CV_ASP.NET.Controllers
{
    public class AnvsidaController : BasController
    {
        private TestDataContext testDb;
        public AnvsidaController(TestDataContext _context) 
        {
            testDb = _context;
        }
        public IActionResult AnvSida(String Anvid)
        {
            String inloggadAnv = base.HamtaAnv();
            String anonymAnv =Anvid ?? inloggadAnv;

            var anv = testDb.Users.SingleOrDefault(u => u.Id == anonymAnv);
            var adress = testDb.Adresser.SingleOrDefault(a => a.Anvid == anonymAnv);
            var cv = testDb.CV.SingleOrDefault(c => c.AnvandarNamn == anonymAnv);

            if (anv == null )
            {
                return NotFound("Användare hittades inte.");
            }

            if (adress == null)
            {
                return NotFound("Adressen hittades inte.");
            }


            var AnvandarSida = new AnvandarSidaViewModel
            {
                anvandare = anv,
                CV = cv,
                InloggadAnvandare = inloggadAnv,
                adress= adress

            };
            return View(AnvandarSida);

        }
    }
}

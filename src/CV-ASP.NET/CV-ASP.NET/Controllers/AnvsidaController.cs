using CV_ASP.NET.DataContext;
using CV_ASP.NET.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CV_ASP.NET.Controllers
{
    public class AnvsidaController : BasController
    {
        private TestDataContext testDb;
        private readonly UserManager<Anvandare> _hanteraAnv;
        public AnvsidaController(TestDataContext _context, UserManager<Anvandare> hanteraAnv) 
        {
            testDb = _context;
            _hanteraAnv= hanteraAnv;
        }
        public async Task<IActionResult> AnvSidaAsync(String Anvid)
        {
            String inloggadAnv = base.HamtaAnv();
            String anonymAnv =Anvid ?? inloggadAnv;

            var anv = testDb.Users.SingleOrDefault(u => u.Id == anonymAnv);
            var adress = testDb.Adresser.SingleOrDefault(a => a.Anvid == anonymAnv);
            var cv = testDb.CV.SingleOrDefault(c => c.AnvandarNamn == anonymAnv);
            

            if (anv == null)
            {
                ViewData["ErrorMessage"] = "Användare hittades inte.";
                return View(new AnvandarSidaViewModel()); // Returnerar den aktuella vyn med felmeddelandet.
            }

            //if (adress == null)
            //{
            //    ViewData["ErrorMessage"] = "Adress hittades inte.";
            //    return View(new AnvandarSidaViewModel()); // Returnerar den aktuella vyn med felmeddelandet.
            //}
            var identifieradAnv = await _hanteraAnv.FindByIdAsync(anv.Id);
            var AnvEmail = identifieradAnv?.Email;

            var AnvandarSida = new AnvandarSidaViewModel
            {
                anvandare = anv,
                CV = cv,
                InloggadAnvandare = inloggadAnv,
                adress= adress,
                epost= AnvEmail

            };
            return View(AnvandarSida);

        }
    }
}

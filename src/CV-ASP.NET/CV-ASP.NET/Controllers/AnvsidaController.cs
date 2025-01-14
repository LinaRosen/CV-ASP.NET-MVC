using CV_ASP.NET.DataContext;
using CV_ASP.NET.Models;
using CV_ASP.NET.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlTypes;
using Microsoft.EntityFrameworkCore;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CV_ASP.NET.Controllers
{
    public class AnvsidaController : BasController
    {
        private TestDataContext testDb;
        private readonly UserManager<Anvandare> _hanteraAnv;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AnvsidaController(TestDataContext _context, UserManager<Anvandare> hanteraAnv, IWebHostEnvironment webHostEnviroment) : base(_context)
        {
            testDb = _context;
            _hanteraAnv= hanteraAnv;
            _webHostEnvironment = webHostEnviroment;
        }

        
        public async Task<IActionResult> AnvSidaAsync(string Anvid)
        {
            string inloggadAnv = base.HamtaAnv();
            //String anonymAnv = Anvid ?? inloggadAnv;

            var anv = await testDb.Users
              .Include(u => u.CV) // Lägger till CV-information
              .SingleOrDefaultAsync(u => u.Id == inloggadAnv);
            /*= testDb.Users.SingleOrDefault(u => u.Id == inloggadAnv);*/

            //var adress = testDb.Adresser.SingleOrDefault(a => a.Anvid == inloggadAnv);
            var cv = testDb.CV.SingleOrDefault(c => c.AnvandarNamn == inloggadAnv);


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
                anvandare = anv, // Skicka hela listan av användare till vyn
                InloggadAnvandare = inloggadAnv,
                CV = anv.CV,
                //anvandare = anv,
                //CV = anv.CV,
                //InloggadAnvandare = inloggadAnv,
                //adress= adress,
                //epost= AnvEmail

            };
            return View(AnvandarSida);

        }
        public IActionResult AnvSidaSync(string id)
        {
            var anvandare = testDb.Anvandare.FirstOrDefault(a => a.Id == id);
            if (anvandare == null)
            {
                return NotFound();
            }

            var cv = testDb.CV.FirstOrDefault(c => c.AnvandarNamn == anvandare.UserName); // Associera CV med användare

            var viewModel = new AnvandarSidaViewModel
            {
                anvandare = anvandare,
                CV = cv,
                InloggadAnvandare = id
            };

            return View("AnvSida", viewModel); // Här specificerar vi att vyn som ska visas är "AnvSida"
        }


    }
}

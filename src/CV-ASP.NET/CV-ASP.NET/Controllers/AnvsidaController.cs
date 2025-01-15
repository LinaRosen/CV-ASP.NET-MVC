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

        public AnvsidaController(TestDataContext _context, UserManager<Anvandare> hanteraAnv, IWebHostEnvironment webHostEnviroment) 
        {
            testDb = _context;
            _hanteraAnv= hanteraAnv;
            _webHostEnvironment = webHostEnviroment;
        }

        // Hämtar användarens information, inklusive CV och projekt, och returnerar en vy med användarens profil och CV-sida.
        public async Task<IActionResult> AnvSidaAsync(string Anvid)
        {
            string inloggadAnv = base.HamtaAnv();

            var anv = await testDb.Users
              .Include(u => u.CV) 
              .SingleOrDefaultAsync(u => u.Id == inloggadAnv);
        
            var cv = testDb.CV.SingleOrDefault(c => c.AnvandarNamn == inloggadAnv);

            if (anv == null)
            {
                ViewData["ErrorMessage"] = "Användare hittades inte.";
                return View(new AnvandarSidaViewModel()); 
            }

            var identifieradAnv = await _hanteraAnv.FindByIdAsync(anv.Id);
            var AnvEmail = identifieradAnv?.Email;

            var AnvandarSida = new AnvandarSidaViewModel
            {
                anvandare = anv,
                InloggadAnvandare = inloggadAnv,
                CV = anv.CV,
                Projekt = anv.AnvProjekt,
            };

            return View(AnvandarSida);

        }

        // Hämtar användarens profil och CV baserat på det angivna användar-ID och returnerar en vy med användarens profilinformation och CV.
        public IActionResult AnvSidaSync(string id)
        {
            var anvandare = testDb.Anvandare.FirstOrDefault(a => a.Id == id);
            if (anvandare == null)
            {
                return NotFound();
            }

            var cv = testDb.CV.FirstOrDefault(c => c.AnvandarNamn == anvandare.UserName); 

            var viewModel = new AnvandarSidaViewModel
            {
                anvandare = anvandare,
                CV = cv,
                InloggadAnvandare = id
            };

            return View("AnvSida", viewModel); 
        }
    }
}

using System.Diagnostics;
using CV_ASP.NET.DataContext;
using CV_ASP.NET.Models;
using CV_ASP.NET.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CV_ASP.NET.Controllers
{
    public class HomeController  : BasController
    {
        private TestDataContext testDb;

        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomeController(ILogger<HomeController> logger, TestDataContext dbcontext, IWebHostEnvironment webHostEnvironment) 
        {
            _logger = logger;
            testDb = dbcontext;
            _webHostEnvironment = webHostEnvironment;
        }

        // Hämtar användare med offentliga CV:n och projekt som inte är aktiverade, och skickar dessa till startsidan.
        public IActionResult Index() 
        {
            StartsidaViewModel model = new StartsidaViewModel { };

            model.Anvandare = testDb.Anvandare
                .Where(u => !u.PrivatProfil)
                .Where(u => u.CV != null) 
                .Where(u => !u.Aktiverad)
                .Include(u => u.CV) 
                .Take(4)
                .ToList();

            model.Projekt = testDb.Projekt
                    .Include(p => p.AnvProjekt) 
                        .ThenInclude(ap => ap.Anvandare) 
                    .OrderByDescending(p => p.DatumSkapad) 
                    .ToList();

            return View(model);
        }

        
        public IActionResult LoggaIn()
        {
            return View();
        }

        public IActionResult Registrera()
        {
            return View();
        }

        public IActionResult AnvSida()
        {
            return View();
        }


        // Visar en felvy med ett unikt request-ID för att underlätta felsökning
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        [HttpGet]
        public IActionResult Add()
        {
            Anvandare nyanvandare = new Anvandare();
            List<SelectListItem> allaAnvandare = testDb.Anvandare.Select
            (x => new SelectListItem
            {
                Text = x.Anvandarnamn,
                Value = x.Id.ToString()
            }).ToList();
            ViewBag.options = allaAnvandare;
            return View(nyanvandare);
        }

      
    }
}

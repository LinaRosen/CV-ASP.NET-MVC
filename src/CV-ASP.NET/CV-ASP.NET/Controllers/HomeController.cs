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
    public class HomeController  : Controller
    {
        private TestDataContext testDb;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, TestDataContext dbcontext)
        {
            _logger = logger;
            testDb = dbcontext;
        }

        public IActionResult Index()
            //Allt detta ska ändras, bara kopierat för att få en grund. 
        {
            StartsidaViewModel model = new StartsidaViewModel { };

            //Hämtar 4 CVn från databasen
            model.Anvandare = testDb.Anvandare.Where(u => !u.PrivatProfil).Where(u => u.CV != null).Where(u => !u.Aktiverad).Take(4).ToList();

            //Hämtar det senaste projektet och sorterar genom datum de skapades (fallande) samt konverterar resultatet till lista
            model.Projekt = testDb.Projekt
                .OrderByDescending(p => p.DatumSkapad)
                .Take(1)
                .ToList();

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult LoggaIn()
        {
            return View();
        }

        public IActionResult Registrera()
        {
            return View();
        }

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

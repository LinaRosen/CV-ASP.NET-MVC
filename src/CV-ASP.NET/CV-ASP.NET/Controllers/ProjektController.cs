using CV_ASP.NET.DataContext;
using CV_ASP.NET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CV_ASP.NET.Controllers
{
    public class ProjektController : BasController
    {
        private readonly TestDataContext _context;
        private readonly UserManager<Anvandare> _skapadAv;

        public ProjektController(TestDataContext context, UserManager<Anvandare> skapadAv)
        {
            _context = context;
            _skapadAv = skapadAv;
        }

        [Authorize]
        public IActionResult SkapaProjekt()
        {
            return View("SkapaProjekt");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Skapa(Projekt projekt)
        {
            var anvandarId = HamtaAnv();
            projekt.SkapadAv = anvandarId;
            projekt.Anvandare = _context.Anvandare.SingleOrDefault(a => a.Id == anvandarId);
            if (ModelState.IsValid)
            {
                projekt.SkapadAv = HamtaAnv();
                projekt.DatumSkapad = DateOnly.FromDateTime(DateTime.Now);

                _context.Projekt.Add(projekt);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Projektet har skapats framgångsrikt!";

                return RedirectToAction("SkapaProjekt");
            }

            return View("SkapaProjekt", projekt);
        }

    }


}

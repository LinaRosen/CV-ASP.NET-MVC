using CV_ASP.NET.DataContext;
using CV_ASP.NET.Models;
using CV_ASP.NET.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                projekt.DatumSkapad = DateOnly.FromDateTime(DateTime.Now);

                _context.Projekt.Add(projekt);
                _context.SaveChanges();

                var anvProjekt = new AnvProjekt
                {
                    Anvid = anvandarId,
                    Pid = projekt.Pid
                };
                _context.AnvProjekt.Add(anvProjekt);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Projektet har skapats framgångsrikt!";
                return RedirectToAction("SkapaProjekt");
            }

            return View("SkapaProjekt", projekt);
        }

        [HttpGet]
        public IActionResult ListaProjekt()
        {
            var inloggadAnvId = User.Identity.IsAuthenticated ? HamtaAnv() : null;

            if (inloggadAnvId != null)
            {
                var projekt = _context.Projekt
                    .Include(p => p.Anvandare)
                    .Include(p => p.AnvProjekt)
                        .ThenInclude(ap => ap.Anvandare)
                    .Select(p => new ProjektVy
                    {
                        Id = p.Pid, 
                        Namn = p.Namn,
                        Beskrivning = p.Beskrivning,
                        DatumSkapad = p.DatumSkapad,
                        SkapadAvInloggadAnv = p.SkapadAv == inloggadAnvId,
                        ArMedlem = p.AnvProjekt.Any(ap => ap.Anvid == inloggadAnvId),
                        Medlemmar = p.AnvProjekt
                            .Where(ap => ap.Anvandare != null)
                            .Select(ap => $"{ap.Anvandare.Fornamn} {ap.Anvandare.Efternamn}")
                            .ToList()
                    })
                    .ToList();

                return View(projekt);
            }
            else
            {
                var projekt = _context.Projekt
                    .Include(p => p.Anvandare)
                    .Include(p => p.AnvProjekt)
                        .ThenInclude(ap => ap.Anvandare)
                    .Where(p => p.AnvProjekt.Any(ap =>
                        ap.Anvandare != null &&
                        !ap.Anvandare.PrivatProfil))
                    .Select(p => new ProjektVy
                    {
                        Id = p.Pid, 
                        Namn = p.Namn,
                        Beskrivning = p.Beskrivning,
                        DatumSkapad = p.DatumSkapad,
                        SkapadAvInloggadAnv = false,
                        ArMedlem = false,
                        Medlemmar = p.AnvProjekt
                            .Where(ap => ap.Anvandare != null && !ap.Anvandare.PrivatProfil)
                            .Select(ap => $"{ap.Anvandare.Fornamn} {ap.Anvandare.Efternamn}")
                            .ToList()
                    })
                    .ToList();

                return View(projekt);
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult RedigeraProjekt(int id)
        {
            var projekt = _context.Projekt.SingleOrDefault(p => p.Pid == id);

            if (projekt == null)
            {
                return NotFound("Projektet hittades inte.");
            }

            if (projekt.SkapadAv != HamtaAnv())
            {
                return Forbid("Du har inte behörighet att redigera detta projekt.");
            }

            return View(projekt);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult SparaÄndringar(Projekt projekt)
        {
            if (ModelState.IsValid)
            {
                var befintligtProjekt = _context.Projekt.SingleOrDefault(p => p.Pid == projekt.Pid);

                if (befintligtProjekt == null)
                {
                    return NotFound("Projektet hittades inte.");
                }

                if (befintligtProjekt.SkapadAv != HamtaAnv())
                {
                    return Forbid("Du har inte behörighet att redigera detta projekt.");
                }

                befintligtProjekt.Namn = projekt.Namn;
                befintligtProjekt.Beskrivning = projekt.Beskrivning;

                _context.SaveChanges();

                TempData["SuccessMessage"] = "Projektet har uppdaterats framgångsrikt!";
                return RedirectToAction("ListaProjekt");
            }

            return View("RedigeraProjekt", projekt);
        }



        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult GåMed(int id)
        {
            var inloggadAnvId = HamtaAnv(); 

            if (!_context.AnvProjekt.Any(ap => ap.Anvid == inloggadAnvId && ap.Pid == id))
            {
                var anvProjekt = new AnvProjekt
                {
                    Anvid = inloggadAnvId,
                    Pid = id
                };

                _context.AnvProjekt.Add(anvProjekt);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Du har gått med i projektet!";
            }
            else
            {
                TempData["ErrorMessage"] = "Du är redan medlem i projektet.";
            }

            return RedirectToAction("ListaProjekt");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult LämnaProjekt(int id)
        {
            var inloggadAnvId = HamtaAnv(); 

            var anvProjekt = _context.AnvProjekt.SingleOrDefault(ap => ap.Anvid == inloggadAnvId && ap.Pid == id);

            if (anvProjekt != null)
            {
                _context.AnvProjekt.Remove(anvProjekt);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Du har lämnat projektet.";
            }
            else
            {
                TempData["ErrorMessage"] = "Du är inte medlem i detta projekt.";
            }

            return RedirectToAction("ListaProjekt");
        }



    }


}

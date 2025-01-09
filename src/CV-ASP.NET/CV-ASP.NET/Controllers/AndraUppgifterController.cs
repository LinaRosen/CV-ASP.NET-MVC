using CV_ASP.NET.Controllers;
using CV_ASP.NET.DataContext;
using CV_ASP.NET.Models;
using CV_ASP.NET.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CV_ASP.NET.Controllers
{

    public class AndraUppgifterController : BasController
    {
        private readonly UserManager<Anvandare> _userManager;
        private readonly TestDataContext _context;

        public AndraUppgifterController(UserManager<Anvandare> userManager, TestDataContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public async Task <IActionResult> RedigeraUppgifter()
        {
            string? inloggadAnv = base.HamtaAnv();
            if (string.IsNullOrEmpty(inloggadAnv))
            {
                return RedirectToAction("Login", "Account");
            }

            var anvandare = _context.Users.FirstOrDefault(u => u.Id == inloggadAnv);
            var adress = _context.Adresser.FirstOrDefault(a => a.Anvid == inloggadAnv);

            if (anvandare == null || adress == null)
            {
                ModelState.AddModelError("", "Användar- eller adressinformation kunde inte hämtas.");
                return View();
            }

            var model = new RedigeraUppgifterViewModel
            {
                anvandare = anvandare,
                adress = adress
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("RedigeraUppgifter")]
        // Redigera användaruppgifter (GET)
        public async Task<IActionResult> RedigeraUpg(RedigeraUppgifterViewModel model)
        {
            string? inloggadAnv = base.HamtaAnv();
            if (string.IsNullOrEmpty(inloggadAnv))
            {
                return RedirectToAction("Login", "Account");
            }

            var anvandare = _context.Users.FirstOrDefault(u => u.Id == inloggadAnv);
            var adress = _context.Adresser.FirstOrDefault(a => a.Anvid == inloggadAnv);

            if (anvandare == null || adress == null)
            {
                ModelState.AddModelError("", "Användar- eller adressinformation kunde inte hittas.");
                return View(model);
            }

            // Uppdatera användarens information
            anvandare.Anvandarnamn = model.anvandare.Anvandarnamn;
            anvandare.Fornamn = model.anvandare.Fornamn;
            anvandare.Efternamn = model.anvandare.Efternamn;
            anvandare.Email = model.anvandare.Email;
            anvandare.PhoneNumber = model.anvandare.PhoneNumber;
            anvandare.PrivatProfil = model.anvandare.PrivatProfil;

            // Uppdatera adressinformationen
            adress.Gatunamn = model.adress.Gatunamn;
            adress.Stad = model.adress.Stad;
            adress.Postnummer = model.adress.Postnummer;

            // Spara ändringar i databasen
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Dina uppgifter har uppdaterats.";
            return RedirectToAction("Anvsida", "AnvSida"); // Ändra "Profil" och "Anvandare" till rätt vy/kontroller
        }


    }
}

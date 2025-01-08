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

        // Redigera användaruppgifter (GET)
        public async Task<IActionResult> RedigeraUppgifter(string anvId)
        {
            string? inloggadAnv = base.HamtaAnv();

            RedigeraUppgifterViewModel model = new RedigeraUppgifterViewModel();

            if (anvId.Equals(inloggadAnv))
            {
                model.anvandare = _context.Users.FirstOrDefault(u => u.Id == anvId);
                model.adress = _context.Adresser.Where(a => a.Anvid.Equals(inloggadAnv)).FirstOrDefault();
                var adressen = new Adress();

                adressen.Gatunamn = model.adress.Gatunamn;
                adressen.Stad = model.adress.Stad;
                adressen.Postnummer = model.adress.Postnummer;

                var anv = new Anvandare();

                anv.Anvandarnamn = model.anvandare.Anvandarnamn;
                anv.Fornamn = model.anvandare.Fornamn;
                anv.Efternamn = model.anvandare.Efternamn;
                anv.Email = model.anvandare.Email;
                anv.PhoneNumber = model.anvandare.PhoneNumber;
                anv.PrivatProfil = model.anvandare.PrivatProfil;
            }


            else
            {
                ModelState.AddModelError("", "Användaren är inte inloggad.");
                return RedirectToAction("Login", "Account");
            }

            var user = await _userManager.FindByIdAsync(anvId);
            if (user == null)
            {
                ModelState.AddModelError("", "Användaren hittades inte.");
                return View();
            }

            return View(model);
        }
 
    }
}

using CV_ASP.NET.Controllers;
using CV_ASP.NET.DataContext;
using CV_ASP.NET.Models;
using CV_ASP.NET.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CV_ASP.NET.Controllers
{

    public class AndraKontaktController : BasController
    {
        private readonly UserManager<Anvandare> _userManager;
        private readonly TestDataContext _context;
        private SignInManager<Anvandare> signInManager;

        public AndraKontaktController(UserManager<Anvandare> userManager, SignInManager<Anvandare> signInMngr, TestDataContext context)
        {
            _userManager = userManager;
            _context = context;
            signInManager = signInMngr;
        }





        [HttpGet]
        public async Task<IActionResult> RedigeraUppgifter()
        {
            string? inloggadAnv = base.HamtaAnv();
            if (string.IsNullOrEmpty(inloggadAnv))
            {
                return RedirectToAction("Login", "Account");
            }

            var anvandare = await _context.Users.FirstOrDefaultAsync(u => u.Id == inloggadAnv);

            if (anvandare == null)
            {
                ModelState.AddModelError("", "Användar- eller adressinformation kunde inte hämtas.");
                return View();
            }

            var model = new AndraKontaktViewModel
            {
                Anvandarnamn = anvandare.Anvandarnamn,
                Fornamn = anvandare.Fornamn,
                Efternamn = anvandare.Efternamn,
                Email = anvandare.Email,
                Telefonnummer = anvandare.PhoneNumber,
                PrivatProfil = anvandare.PrivatProfil,
                Gatunamn = anvandare.Gatunamn,
                Stad = anvandare.Stad,
                Postnummer = anvandare.Postnummer
            };

            return View(model);
        }


        [HttpPost]
        [ActionName("RedigeraUppgifter")]
        public async Task<IActionResult> RedigeraUppgifter(AndraKontaktViewModel model)
        {
            // Kontrollera modellens giltighet
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string? inloggadAnv = base.HamtaAnv();
            if (string.IsNullOrEmpty(inloggadAnv))
            {
                return RedirectToAction("Login", "Account");
            }

            var anvandare = await _context.Users.FirstOrDefaultAsync(u => u.Id == inloggadAnv);

            if (anvandare == null)
            {
                ModelState.AddModelError("", "Användar- eller adressinformation kunde inte hittas.");
                return View(model);
            }

            // Uppdatera användarens information
            anvandare.Anvandarnamn = model.Anvandarnamn;
            anvandare.UserName = model.Anvandarnamn;  // Detta gör att Identity använder det nya användarnamnet.
            anvandare.Fornamn = model.Fornamn;
            anvandare.Efternamn = model.Efternamn;
            anvandare.Email = model.Email;
            anvandare.PhoneNumber = model.Telefonnummer;
            anvandare.PrivatProfil = model.PrivatProfil;
            anvandare.Gatunamn = model.Gatunamn;
            anvandare.Stad = model.Stad;
            anvandare.Postnummer = model.Postnummer;

            // Uppdatera användaren i databasen
            _context.Anvandare.Update(anvandare);
            await _context.SaveChangesAsync();

            // Uppdatera sessionen med den nya informationen utan att logga ut
            var user = await _userManager.FindByIdAsync(inloggadAnv);
            if (user != null)
            {
                user.UserName = model.Anvandarnamn; // Se till att uppdatera användarnamnet i Identity
                user.PhoneNumber = model.Telefonnummer;
                user.Email = model.Email;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    // Om uppdateringen i Identity lyckades, uppdatera sessionen
                    await signInManager.SignInAsync(user, isPersistent: false);
                }
                else
                {
                    // Hantera eventuella problem med uppdateringen
                    ModelState.AddModelError("", "Något gick fel vid uppdatering av användarinformation.");
                    return View(model);
                }
            }

            // Informera användaren om att uppdateringarna har genomförts
            TempData["SuccessMessage"] = "Dina uppgifter har uppdaterats.";
            return RedirectToAction("Anvsida", "AnvSida");
        }



        [HttpGet]
        public IActionResult AndraLosenord()
        {
            return View();
        }

        // Hanterar POST-förfrågan för att ändra användarens lösenord efter att ha verifierat nuvarande lösenord och säkerställt att de nya lösenorden matchar.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AndraLosenord(AndraLosenordViewModel model)
        {
            string? inloggadAnv = base.HamtaAnv();
            if (string.IsNullOrEmpty(inloggadAnv))
            {
                return RedirectToAction("Login", "Account");
            }

            var anvandare = await _userManager.FindByIdAsync(inloggadAnv);

            if (anvandare == null)
            {
                ModelState.AddModelError("", "Användaren kunde inte hittas.");
                return View(model);
            }

            if (!await _userManager.CheckPasswordAsync(anvandare, model.NuvarandeLosenord))
            {
                TempData["ErrorMessage"] = "Det nuvarande lösenordet är felaktigt.";
                return View(model);
            }

            if (model.NyttLosenord != model.BekraftaNyttLosenord)
            {
                TempData["ErrorMessage"] = "De nya lösenorden matchar inte.";
                return View(model);
            }

            var result = await _userManager.ChangePasswordAsync(anvandare, model.NuvarandeLosenord, model.NyttLosenord);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Ditt lösenord har ändrats.";
            }
            else
            {
                TempData["ErrorMessage"] = "Lösenordet kunde inte ändras. Försök igen.";
            }

            return View(model);
        }
    }
}


﻿using CV_ASP.NET.Controllers;
using CV_ASP.NET.DataContext;
using CV_ASP.NET.Models;
using CV_ASP.NET.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // Hämtar och visar användarens information för redigering om användaren är inloggad
        [HttpGet]
        public async Task <IActionResult> RedigeraUppgifter()
        {
            string? inloggadAnv = base.HamtaAnv();
            if (string.IsNullOrEmpty(inloggadAnv))
            {
                return RedirectToAction("Login", "Account");
            }

            var anvandare = _context.Users.FirstOrDefault(u => u.Id == inloggadAnv);

            if (anvandare == null)
            {
                ModelState.AddModelError("", "Användar- eller adressinformation kunde inte hämtas.");
                return View();
            }

            var model = new RedigeraUppgifterViewModel
            {
                anvandare = anvandare
            };

            return View(model);
        }

        // Hanterar POST-förfrågan för att uppdatera användarens information och spara ändringarna i databasen.
        [HttpPost]
        [ActionName("RedigeraUppgifter")]
        public async Task<IActionResult> RedigeraUpg(RedigeraUppgifterViewModel model)
        {
            string? inloggadAnv = base.HamtaAnv();
            if (string.IsNullOrEmpty(inloggadAnv))
            {
                return RedirectToAction("Login", "Account");
            }

            var anvandare = _context.Users.FirstOrDefault(u => u.Id == inloggadAnv);

            if (anvandare == null)
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
            anvandare.Gatunamn = model.anvandare.Gatunamn;
            anvandare.Stad = model.anvandare.Stad;
            anvandare.Postnummer = model.anvandare.Postnummer;
     
            await _context.SaveChangesAsync();

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


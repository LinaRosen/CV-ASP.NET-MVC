using CV_ASP.NET.Models;
using CV_ASP.NET.Models.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CV_ASP.NET.DataContext;

namespace CV_ASP.NET.Controllers
{
    public class LoggInRegistreraController:Controller
    {
        private UserManager<Anvandare> userManager;
        private SignInManager<Anvandare> signInManager;
        private readonly TestDataContext _context;
        public LoggInRegistreraController(UserManager<Anvandare> userMngr,
        SignInManager<Anvandare> signInMngr, TestDataContext context)
        {
            this.userManager = userMngr;
            this.signInManager = signInMngr;
            this._context = context;
        }

        [HttpGet]
        public IActionResult Registrera()
        {
            return View();
        }

        // Hanterar användarregistrering, skapar användare och loggar in vid framgång, annars visas felmeddelanden.
        [HttpPost]
        public async Task<IActionResult> Registrera(RegistreraViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                Anvandare anvandare = new Anvandare()
                {
                    UserName = registerViewModel.Anvandarnamn,
                    Anvandarnamn = registerViewModel.Anvandarnamn,
                    Email = registerViewModel.Email,
                    Efternamn = registerViewModel.Efternamn,
                    Fornamn = registerViewModel.Fornamn,
                    PhoneNumber = registerViewModel.Telefonnummer,
                    PrivatProfil = registerViewModel.PrivatProfil,
                    Gatunamn = registerViewModel.Gatunamn,
                    Stad = registerViewModel.Stad,
                    Postnummer = registerViewModel.Postnummer
                };

                var result = await userManager.CreateAsync(anvandare, registerViewModel.Losenord);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(anvandare, isPersistent: true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(registerViewModel);
        }


        [HttpGet]
        public IActionResult LoggaIn()
        {
            LoggaInViewModel loginViewModel = new LoggaInViewModel();
            return View(loginViewModel);
        }

        // Hanterar användarinloggning, autentiserar med användarnamn och lösenord, och om lyckad inloggning, omdirigerar till startsidan.
        [HttpPost]
        public async Task<IActionResult> LoggaIn(LoggaInViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                loginViewModel.Anvandarnamn,
                loginViewModel.Losenord,
                isPersistent: loginViewModel.RememberMe,
                lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Fel användarnam/lösenord.");
                }
            }
            return View(loginViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        }
    }



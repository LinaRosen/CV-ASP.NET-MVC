using CV_ASP.NET.DataContext;
using CV_ASP.NET.Models;
using CV_ASP.NET.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CV_ASP.NET.Controllers
{
    public class SokController : Controller
    {
        private readonly TestDataContext _context;

        public SokController(TestDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(string searchString)
        {
            // Börja med en tom lista för filtrerade användare
            var filtreradeAnvandare = new List<Anvandare>();

            // Kontrollera om söksträngen är tom
            if (!string.IsNullOrEmpty(searchString))
            {
                string[] soktermer = searchString.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                // Hämta alla användare från databasen
                var allaAnvandare = _context.Anvandare.ToList();

                // Filtrera användare baserat på söksträng
                filtreradeAnvandare = allaAnvandare
                    .Where(a => soktermer.Any(term =>
                        a.Fornamn.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                        a.Efternamn.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                        a.Anvandarnamn.Contains(term, StringComparison.OrdinalIgnoreCase)))
                    .ToList();

                // Om användaren inte är inloggad, ta bort privata användare
                if (!User.Identity.IsAuthenticated)
                {
                    filtreradeAnvandare = filtreradeAnvandare
                        .Where(a => !a.PrivatProfil) // Filtrera bort privata profiler
                        .ToList();
                }
            }

            // Skapa en SokViewModel för vyn
            var viewModel = new SokViewModel
            {
                Anvandare = filtreradeAnvandare
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AnvSida(string userId)
        {
            // Kontrollera att parametern inte är null eller tom
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID is required.");
            }

            // Hämta användaren från databasen
            var anvandare = await _context.Anvandare
                .Include(a => a.CV) // Ladda CV för användaren om det finns
                .Include(a => a.Adress) // Ladda Adress om det finns
                .FirstOrDefaultAsync(a => a.Id == userId);

            // Kontrollera om användaren hittades
            if (anvandare == null)
            {
                return NotFound("Användaren hittades inte.");
            }

            // Skapa ViewModel för användarsidan
            var viewModel = new AnvandarSidaViewModel
            {
                anvandare = anvandare,
                CV = anvandare.CV,
                adress = anvandare.Adress,
                epost = anvandare.Email
            };

            // Returnera vyn med ViewModel
            return View(viewModel);
        }






    }
}

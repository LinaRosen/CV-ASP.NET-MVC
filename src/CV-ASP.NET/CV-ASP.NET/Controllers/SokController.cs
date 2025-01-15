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
    public class SokController : BasController
    {
        private readonly TestDataContext _context;

        public SokController(TestDataContext context) 
        {
            _context = context;
        }

        // Hämtar och filtrerar användare baserat på söksträng och tillgänglig profiltyp (offentlig eller privat).
        [HttpGet]
        public IActionResult Index(string searchString)
        {
            var filtreradeAnvandare = new List<Anvandare>();

            if (!string.IsNullOrEmpty(searchString))
            {
                string[] soktermer = searchString.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                var allaAnvandare = _context.Anvandare.ToList();

                filtreradeAnvandare = allaAnvandare
                    .Where(a => soktermer.Any(term =>
                        a.Fornamn.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                        a.Efternamn.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                        a.Anvandarnamn.Contains(term, StringComparison.OrdinalIgnoreCase)))
                    .ToList();

                if (!User.Identity.IsAuthenticated)
                {
                    filtreradeAnvandare = filtreradeAnvandare
                        .Where(a => !a.PrivatProfil) 
                        .ToList();
                }
            }

            var viewModel = new SokViewModel
            {
                Anvandare = filtreradeAnvandare
            };

            return View(viewModel);
        }
    }
}

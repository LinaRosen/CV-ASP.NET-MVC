using System.Security.Claims;
using CV_ASP.NET.Controllers;
using CV_ASP.NET.DataContext;
using CV_ASP.NET.Models;
using CV_ASP.NET.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class MeddelandeController : BasController
{
    private readonly TestDataContext _testDb;
    private readonly UserManager<Anvandare> _hanteraAnv;

    public MeddelandeController(TestDataContext context, UserManager<Anvandare> hanteraAnv)
    {
        _testDb = context;
        _hanteraAnv = hanteraAnv;
    }

    [HttpGet]
    public IActionResult SkickaMeddelande(string tillAnvandareId)
    {
        var viewModel = new SkickaMeddelandeViewModel
        {
            TillAnvandareId = tillAnvandareId
        };

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult SkickaMeddelande(SkickaMeddelandeViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model); // Returnera med felmeddelanden om modellen är ogiltig
        }

        // Kontrollera om mottagar-ID är angivet
        if (string.IsNullOrEmpty(model.TillAnvandareId))
        {
            ModelState.AddModelError("TillAnvandareId", "Mottagare saknas.");
            return View(model);
        }

        var meddelande = new Meddelande
        {
            TillAnvandareId = model.TillAnvandareId,
            Innehall = model.Innehall,
            Last = false,
        };

        if (User.Identity.IsAuthenticated)
        {
            meddelande.FranAnvandareId = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier);
            meddelande.AnonymAnvandare = null; // Nollställ anonymt namn för inloggad användare
        }
        else
        {
            meddelande.AnonymAnvandare = model.AnonymAnvandare;
        }

        _testDb.Meddelande.Add(meddelande);
        _testDb.SaveChanges();

        TempData["SuccessMessage"] = "Ditt meddelande har skickats!";
        return RedirectToAction("MeddelandeSida");
    }

    public async Task<IActionResult> MeddelandeSida()
    {
        var anvString = base.HamtaAnv();
        if (string.IsNullOrEmpty(anvString))
        {
            return RedirectToAction("Login", "Account");
        }

        var anv = _testDb.Users.SingleOrDefault(u => u.Id == anvString);
        if (anv == null)
        {
            return NotFound();
        }

        var meddelanden = _testDb.Meddelande.Where(m => m.TillAnvandareId == anv.Id).ToList();

        var viewModel = new MeddelandeViewModel
        {
            Meddelanden = meddelanden
        };

        return View(viewModel);
    }
    [HttpGet]
    public IActionResult Kontakta(string tillAnvandareId)
    {
        // Skapa ViewModel och skicka TillAnvandareId till vyn
        var viewModel = new SkickaMeddelandeViewModel
        {
            TillAnvandareId = tillAnvandareId
        };

        // Returnera vyn med ViewModel
        return View("SkickaMeddelande", viewModel);  // Detta borde vara din befintliga vy
    }
}
/*using CV_ASP.NET.Controllers;
using CV_ASP.NET.DataContext;
using CV_ASP.NET.Models;
using CV_ASP.NET.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class MeddelandeController : BasController
{
    private TestDataContext testDb;
    private readonly UserManager<Anvandare> _hanteraAnv;

    public MeddelandeController(TestDataContext _context, UserManager<Anvandare> hanteraAnv)
    {
        testDb = _context;
        _hanteraAnv = hanteraAnv;
    }

    // Denna metod hämtar meddelanden och skickar en ViewModel till vyn
    public async Task<IActionResult> MeddelandeSida()
    {
        // Hämta den inloggade användaren
        var anvString = base.HamtaAnv();
        if (string.IsNullOrEmpty(anvString))
        {
            return RedirectToAction("Login", "Account"); // Redirect till login om användaren inte är inloggad
        }

        var anv = testDb.Users.SingleOrDefault(u => u.Id == anvString);

        if (anv == null)
        {
            return NotFound();
        }

        // Hämta alla meddelanden för användaren komenterat bort för att testa hårdkoda
        //var meddelanden = testDb.Meddelande.Where(m => m.TillAnvandareId == anv.Id).ToList();
        var meddelanden = new List<Meddelande>
        {
            new Meddelande
            {
                Mid = 1,
                Innehall = "Hej! Hur mår du?",
                Last = false,
                FranAnvandareId = "1",
                TillAnvandareId = "2",
                AnonymAnvandare = "Anonym",
                Frananvandare = new Anvandare (),
                Tillanvandare = new Anvandare ()
            },
            new Meddelande
            {
                Mid = 2,
                Innehall = "Hej, jag vill prata senare.",
                Last = true,
                FranAnvandareId = "2",
                TillAnvandareId = "1",
                AnonymAnvandare = "John",
                Frananvandare = new Anvandare (),
                Tillanvandare = new Anvandare ()
            }
        };

        // Skapa en MeddelandeViewModel
        var viewModel = new MeddelandeViewModel
        {
            Meddelanden = meddelanden
        };

        // Skicka ViewModelen till vyn
        return View(viewModel);
    }

    public async Task<int> GetOlastaMeddelanden()
    {
        var anvString = base.HamtaAnv();

        var anv = testDb.Users.SingleOrDefault(u => u.Id == anvString);

        var antalOlasta = 0;
        foreach (var item in anv.TagitEmotMeddelande)
        {
            if (item.Last.HasValue && !item.Last.Value)
            {
                antalOlasta++;
            }
        };
         
        return antalOlasta;
    }

    [HttpPost]
    [Route("Meddelande/MarkeraSomLast")]
    public async Task<IActionResult> MarkeraSomLast(int meddelandeId)
    {

        // Hämta meddelandet baserat på ID
        var meddelande = testDb.Meddelande.SingleOrDefault(m => m.Mid == meddelandeId);

        if (meddelande == null)
        {
            return NotFound("Meddelande hittades ej");
        }

        // Uppdatera lässtatus (markera som läst)
        meddelande.Last = true;
        testDb.SaveChanges(); // Spara ändringen i databasen

        // Också hämta alla meddelanden igen och skicka tillbaka till vyn
        var anvString = base.HamtaAnv();
        var anv = testDb.Users.SingleOrDefault(u => u.Id == anvString);
        var meddelanden = testDb.Meddelande.Where(m => m.TillAnvandareId == anv.Id).ToList();

        var viewModel = new MeddelandeViewModel
        {
            Meddelanden = meddelanden
        };

        return View("MeddelandeSida", viewModel); // Skicka tillbaka till vyn med uppdaterade meddelanden
    }
    [HttpGet]
    public IActionResult SkickaMeddelande(string tillAnvandareId)
    {
        // Skapa ViewModel och skicka den till vyn
        var viewModel = new SkickaMeddelandeViewModel
        {
            TillAnvandareId = tillAnvandareId
        };

        return View(viewModel);
    }
    [HttpGet]
    public IActionResult Kontakta(string tillAnvandareId)
    {
        // Skapa ViewModel och skicka TillAnvandareId till vyn
        var viewModel = new SkickaMeddelandeViewModel
        {
            TillAnvandareId = tillAnvandareId
        };

        // Returnera vyn med ViewModel
        return View("SkickaMeddelande", viewModel);  // Detta borde vara din befintliga vy
    }
    [HttpPost]
    public IActionResult SkickaMeddelande(SkickaMeddelandeViewModel model)
    {
        // Validera modellen
        if (!ModelState.IsValid)
        {
            return View(model); // Om ogiltig, återvänd till samma vy med felmeddelanden
        }

        // Skapa ett nytt Meddelande från ViewModel
        var meddelande = new Meddelande
        {
            TillAnvandareId = model.TillAnvandareId,
            Innehall = model.Innehall,
            Last = false // Nya meddelanden är alltid olästa
        };

        // Om användaren är inloggad, fyll i deras namn
        if (User.Identity.IsAuthenticated)
        {
            meddelande.FranAnvandareId = User.Identity.Name;
            meddelande.AnonymAnvandare = null; // Rensa anonym om det är en inloggad användare
        }
        else
        {
            // Om användaren inte är inloggad, använd det anonyma namnet från formuläret
            meddelande.AnonymAnvandare = model.AnonymAnvandare;
        }

        // Kontrollera om en mottagare är angiven
        if (string.IsNullOrEmpty(meddelande.TillAnvandareId))
        {
            ModelState.AddModelError("TillAnvandareId", "Mottagare saknas.");
            return View(model); // Om ingen mottagare angavs, visa felmeddelande
        }

        // Spara meddelandet i databasen
        testDb.Meddelande.Add(meddelande);
        testDb.SaveChanges();

        // Skicka framgångsmeddelande och omdirigera till meddelandesidan
        TempData["SuccessMessage"] = "Ditt meddelande har skickats!";
        return RedirectToAction("MeddelandeSida");
    }

    /*[HttpPost]
    public IActionResult SkickaMeddelandeForm(SkickaMeddelandeViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Hantera meddelandet här, exempelvis spara i databasen
            return RedirectToAction("Index", "Home");
        }

        return View(model);  // Om ogiltig, återvänd till samma vy
    }


    [HttpPost]
    public IActionResult SkickaMeddelande(Meddelande meddelande)
    {
        if (!ModelState.IsValid)
        {
            return View(meddelande);
        }

        // Om användaren är inloggad, fyll i deras namn
        if (User.Identity.IsAuthenticated)
        {
            meddelande.FranAnvandareId = User.Identity.Name;
            meddelande.AnonymAnvandare = null; // Rensa anonym om det är en inloggad användare
        }

        if (string.IsNullOrEmpty(meddelande.TillAnvandareId))
        {
            ModelState.AddModelError("TillAnvandareId", "Mottagare saknas.");
            return View(meddelande); // Om ingen mottagare angavs, visa felmeddelande
        }

        meddelande.Last = false; // Nya meddelanden är alltid olästa

        testDb.Meddelande.Add(meddelande);
        testDb.SaveChanges();

        TempData["SuccessMessage"] = "Ditt meddelande har skickats!";
        return RedirectToAction("MeddelandeSida");

        /*[HttpGet]
        public IActionResult Kontakta(string tillAnvandareId)
        {
            var viewModel = new SkickaMeddelandeViewModel
            {
                TillAnvandareId = tillAnvandareId
            };

            return View(viewModel);
        }*/



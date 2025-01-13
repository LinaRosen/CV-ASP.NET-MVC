
using System.Linq;
using System.Security.Claims;
using CV_ASP.NET.Controllers;
using CV_ASP.NET.DataContext;
using CV_ASP.NET.Migrations;
using CV_ASP.NET.Models;
using CV_ASP.NET.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        var viewModel = new MeddelandeViewModel
        {
            TillAnvandareId = tillAnvandareId
        };
        TempData.Remove("SuccessMessage");
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult SkickaMeddelande(SkickaMeddelandeViewModel model)
    {
        // Validera att nödvändiga fält är ifyllda
        if (string.IsNullOrWhiteSpace(model.Innehall) ||
            string.IsNullOrWhiteSpace(model.TillAnvandareId))
        {
            ModelState.AddModelError(string.Empty, "Alla obligatoriska fält måste fyllas i.");
            return View(model); // Returnera med felmeddelanden om modellen är ogiltig
        }

        // Kontrollera om AnonymAnvandare är tomt för icke-inloggade användare
        if (!User.Identity.IsAuthenticated && string.IsNullOrEmpty(model.AnonymAnvandare))
        {
            ModelState.AddModelError("AnonymAnvandare", "Du måste ange ditt namn som anonym användare.");
            return View(model); // Returnera med felmeddelanden
        }

        // Kontrollera om mottagar-ID är angivet
        if (string.IsNullOrEmpty(model.TillAnvandareId))
        {
            ModelState.AddModelError("TillAnvandareId", "Mottagare saknas.");
            return View(model);
        }

        // Bestäm avsändaren: Om inloggad användare, använd deras användarnamn, annars använd anonymt namn
        string avsandare;
        if (User.Identity.IsAuthenticated)
        {
            // Om inloggad användare, sätt avsändaren till den inloggades namn
            avsandare = User.Identity.Name;
        }
        else
        {
            // Om inte inloggad, sätt avsändaren till det namn som den anonyma användaren fyllt i
            avsandare = model.AnonymAnvandare;
        }

        // Kontrollera att avsändare är satt korrekt
        if (string.IsNullOrWhiteSpace(avsandare))
        {
            ModelState.AddModelError("Avsandare", "Avsändaren kan inte vara tom.");
            return View(model);
        }

        // Skapa ett nytt meddelande med avsändaren korrekt satt
        var meddelande = new Meddelande
        {
            Mid = model.Mid,
            TillAnvandareId = model.TillAnvandareId,
            Innehall = model.Innehall,

            Last = false
        };

        // Lägg till meddelandet i databasen
        _testDb.Meddelande.Add(meddelande);
        _testDb.SaveChanges();

        // Skicka tillbaka framgångsmeddelande
        TempData["SuccessMessage"] = "Ditt meddelande har skickats!";

        // Efter att meddelandet har skickats, återgå till rätt sida baserat på inloggning
        if (User.Identity.IsAuthenticated)
        {
            return RedirectToAction("MeddelandeSida");
        }
        else
        {
            // Om anonym användare, återställ modellen för att tömma formuläret
            var tomModel = new SkickaMeddelandeViewModel
            {
                TillAnvandareId = model.TillAnvandareId,  // Behåll mottagarens ID
                Innehall = string.Empty,                   // Töm innehållsfältet
                AnonymAnvandare = string.Empty            // Töm anonymt namn-fältet
            };
            return View(tomModel); // Skicka en tom modell till vyn
        }
    }

    public async Task<IActionResult> MeddelandeSida(string anvid)
    {
        MeddelandeViewModel mvm = new MeddelandeViewModel { };
        string inloggadAnv = base.HamtaAnv();

        if (anvid == null || anvid.Equals(inloggadAnv))
        {
            mvm.anv = _testDb.Users.Where(a => a.Id.Equals(inloggadAnv)).FirstOrDefault();
            mvm.Meddelanden = _testDb.Meddelande
               .Where(m => m.TillAnvandareId.Equals(inloggadAnv))
               .Select(m => new Meddelande
               {
                   Mid = m.Mid,
                   TillAnvandareId = m.TillAnvandareId,
                   FranAnvandareId = m.FranAnvandareId,
                   Innehall = m.Innehall ?? "Inget innehåll",
                   //Avsandare = m.Avsandare,
                   OlastaMeddelandenCount = m.OlastaMeddelandenCount,
                   Last = m.Last
               })
               .ToList();
        }


        // Hämta antal olästa meddelanden
        var antalOlasta = _testDb.Meddelande
                                 .Where(m => m.TillAnvandareId == inloggadAnv && m.Last == false)
                                 .Count();

        // Sätt ViewData för olästa meddelanden
        ViewData["OlastaMeddelanden"] = antalOlasta;


        return View(mvm);
    }


    [HttpPost]
    [Route("Meddelande/MarkeraSomLast")]
    public async Task<IActionResult> MarkeraSomLast(int meddelandeId)
    {
        string inloggadAnv = base.HamtaAnv();
        // Hämta meddelandet baserat på ID
        Meddelande meddelande = _testDb.Meddelande.SingleOrDefault(m => m.Mid == meddelandeId);

        if (meddelande == null)
        {
            return NotFound("Meddelande hittades ej");
        }

        // Markera meddelandet som läst
        meddelande.Last = true;

        // Spara ändringen i databasen
        await _testDb.SaveChangesAsync();

        var meddelanden = _testDb.Meddelande
               .Where(m => m.TillAnvandareId.Equals(inloggadAnv))
               .Select(m => new Meddelande
               {
                   Mid = m.Mid,
                   TillAnvandareId = m.TillAnvandareId,
                   FranAnvandareId = m.FranAnvandareId,
                   Innehall = m.Innehall ?? "Inget innehåll",
                   //Avsandare = m.Avsandare,
                   OlastaMeddelandenCount = m.OlastaMeddelandenCount,
                   Last = m.Last
               })
               .ToList();

        // Hämta antal olästa meddelanden
        var antalOlasta = _testDb.Meddelande
                                 .Where(m => m.TillAnvandareId == inloggadAnv && m.Last == false)
                                 .Count();

        _testDb.Update(meddelande);
        _testDb.SaveChangesAsync();
        // Uppdatera ViewData
        ViewData["OlastaMeddelanden"] = antalOlasta;

        var viewModel = new MeddelandeViewModel
        {
            Meddelanden = meddelanden,
            OlastaMeddelandenCount = antalOlasta
        };

        // Skicka tillbaka till vyn med uppdaterade meddelanden och olästa räknaren
        return View("MeddelandeSida", viewModel);

        // Hämta den inloggade användaren
        //var anvString = User.Identity.Name;  // Byt ut base.HamtaAnv() med User.Identity.Name
        //var anv = _testDb.Users.SingleOrDefault(u => u.UserName == anvString);

        //if (anv == null)
        //{
        //    return NotFound("Användaren hittades ej");
        //}

        //// Hämta antalet olästa meddelanden för användaren
        //var antalOlasta = _testDb.Meddelande
        //                         .Where(m => m.TillAnvandareId == anv.Id && m.Last == false)
        //                         .Count();

        //// Hämta alla meddelanden för användaren
        //var meddelanden = _testDb.Meddelande.Where(m => m.TillAnvandareId == anv.Id).ToList();

        //// Konvertera varje Meddelande till en MeddelandeViewModel
        //var meddelandeViewModels = meddelanden.Select(m => new MeddelandeViewModel
        //{
        //    Mid = m.Mid,
        //    TillAnvandareId = m.TillAnvandareId,
        //    Innehall = m.Innehall ?? "Inget innehåll",  // Om Innehåll är null, sätt till "Inget innehåll"
        //    Last = m.Last,
        //    Avsandare = m.Avsandare // Om det är en användare eller anonym, hämta det från meddelandet
        //}).ToList();

        //// Efter att vi har hämtat meddelandena från databasen, kan vi justera avsändaren om den är anonym
        //foreach (var item in meddelandeViewModels) // Ändra 'meddelande' till 'item' för att undvika konflikt
        //{
        //    if (string.IsNullOrEmpty(item.Avsandare)) // Om avsändaren är null eller tomt, sätt den till anonymt
        //    {
        //        item.Avsandare = "Anonym användare";
        //    }
        //    else if (item.Avsandare == anvString) // Om avsändaren är den inloggade användaren, sätt användarnamnet
        //    {
        //        item.Avsandare = anv.Anvandarnamn;
        //    }
        //}

        //// Skapa ViewModel för sidan
        //var viewModel = new MeddelandeSidaViewModel
        //{
        //    Meddelanden = meddelandeViewModels, // Lista av MeddelandeViewModel
        //    OlastaMeddelandenCount = antalOlasta // Antal olästa meddelanden
        //};

        //// Uppdatera ViewData för layouten
        //ViewData["OlastaMeddelanden"] = antalOlasta;

        // Skicka tillbaka till vyn med uppdaterade meddelanden och olästa räknaren
        //return View("MeddelandeSida");
    }


    [HttpGet]
    [Route("api/Meddelande/HamtaOlastraMeddelandenCount/{anvandarnamn}")]
    public IActionResult HamtaOlastraMeddelandenCount(string anvandarnamn)
    {
        if (User.Identity.IsAuthenticated && User.Identity.Name == anvandarnamn)
        {
            var anv = _testDb.Users.SingleOrDefault(u => u.UserName == anvandarnamn);

            if (anv != null)
            {
                var antalOlasta = _testDb.Meddelande
                    .Where(m => m.TillAnvandareId == anv.Id && m.Last == false)
                    .Count();

                // Returnera antalet som JSON
                return Ok(new { AntalOlasta = antalOlasta });
            }

            return NotFound("Användaren hittades inte.");
        }

        return Unauthorized("Du är inte behörig att hämta denna information.");
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




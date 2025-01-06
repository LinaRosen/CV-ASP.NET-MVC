using CV_ASP.NET.Controllers;
using CV_ASP.NET.DataContext;
using CV_ASP.NET.Models;
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
        var meddelande = new Meddelande
        {
            TillAnvandareId = tillAnvandareId
        };

        return View(meddelande);
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

        meddelande.Last = false; // Nya meddelanden är alltid olästa

        testDb.Meddelande.Add(meddelande);
        testDb.SaveChanges();

        TempData["SuccessMessage"] = "Ditt meddelande har skickats!";
        return RedirectToAction("MeddelandeSida");
    }
}

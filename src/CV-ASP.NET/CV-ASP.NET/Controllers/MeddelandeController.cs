
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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
public class MeddelandeController : BasController
{
    private readonly TestDataContext _testDb;
    private readonly UserManager<Anvandare> _hanteraAnv;
    private readonly ILogger<MeddelandeController> _logger;



    public MeddelandeController(TestDataContext context, UserManager<Anvandare> hanteraAnv, ILogger<MeddelandeController> logger)
    {
        _testDb = context;
        _hanteraAnv = hanteraAnv;
        _logger = logger;
    }

    public async Task<string?> HamtaAnvNamn(string anvNamn)
    {
        var anv = await _hanteraAnv.FindByNameAsync(anvNamn);

        return anv?.Id;
    }


    //Hämtar användarnamn med angivet användar-Id och returnerar namnet om användaren finns, annars returneras null.
    public async Task<string?> HamtaAnvId(string anvId)
    {
        var anv = await _hanteraAnv.FindByIdAsync(anvId);
        return anv?.UserName;
    }

    [HttpGet]
    public async Task<IActionResult> SkickaMeddelande()
    {
        _logger.LogInformation("SkickaMeddelande metod anropad.");
        //Hämtar alla användare från databasen och skapar en SelectList som kan användas i vyn
        var anv = await _hanteraAnv.Users.ToListAsync();
        var anvSelect = new SelectList(anv, "Id", "UserName");
        ViewData["TillAnvandareId"] = anvSelect;

        //Hämtar den inloggade användarens ID från Claims och skapar ett nytt meddelandeobjekt
        var inloggadAnv = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var Meddelande = new Meddelande
        {

            Last = false,
            FranAnvandareId = inloggadAnv
        };

        Meddelande.TillAnvandareId = anv.Select(x => x.Id).FirstOrDefault();

        //Returnerar vyn för att skicka meddelanden med det nya meddelandeobjektet
        return View("SkickaMeddelande", Meddelande);
    }

    [HttpPost]
    public async Task<IActionResult> SkickaMeddelande(Meddelande meddelande)
    {
        _logger.LogInformation("SkickaMeddelande metod anropad.");


        

            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                _logger.LogWarning($"ModelState fel: {error.ErrorMessage}");
            }

            //Hämtar mottagarens ID från det valda användarnamnet i vyn
            var tillAnvId = meddelande.TillAnvandareId;
            _logger.LogInformation("Värdet på valdAnv: '{TillAnvandareId}'");


            if (tillAnvId != null)
            {
                //Hämtar den inloggade användarens ID och tilldelar meddelandeobjektet rätt information
                var inloggadAnv = User.FindFirstValue(ClaimTypes.NameIdentifier);
                meddelande.FranAnvandareId = inloggadAnv;
                meddelande.TillAnvandareId = tillAnvId;
                meddelande.Last = false;

                try
                {
                    _logger.LogInformation("Försöker lägga till meddelande i databasen.");
                    _testDb.Meddelande.Add(meddelande);
                    await _testDb.SaveChangesAsync();
                    _logger.LogInformation("Meddelande sparat i databasen.");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Fel vid databasskrivning: {ex.Message}");
                }
                return RedirectToAction("Index", "Home");
            }
        
        

        //Om ModelState inte är giltig, återgår till vyn för att skicka meddelanden
        var anv = await _hanteraAnv.Users.ToListAsync();
        var anvSelectList = new SelectList(anv, "UserName", "UserName");
        ViewData["TillAnvandareId"] = anvSelectList;

        return View("SkickaMeddelande", meddelande);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Inkorg()
    {
        var anvId = base.HamtaAnv();
        List<VisaMeddelande> model = new List<VisaMeddelande>();
        //Hämtar inkommande meddelanden för den inloggade användaren
        var allaMeddelanden = _testDb.Meddelande
            .Where(m => m.TillAnvandareId == anvId)
            .ToList();

        foreach (var meddelande in allaMeddelanden)
        {
            model.Add(new VisaMeddelande
            {
                Meddelande = meddelande,
                FranAnonym = meddelande.AnonymAnvandare,
                FramAnv = await HamtaAnvId(meddelande?.FranAnvandareId)
            });
        }

        IEnumerable<VisaMeddelande> modelToView = model;
        //Associerar varje meddelande med avsändarens användarnamn

        //Returnerar en vy med listan av meddelanden tillsammans med avsändarens användarnamn.
        return View(model);


    }


    //Get-metod som hämtar antalet olästa meddelanden, alltså meddelane där Read är false
    [HttpGet]
    public IActionResult HamtaLasta()
    {
        var anvId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var olastaMed = _testDb.Meddelande.Count(m => m.TillAnvandareId == anvId && m.Last == false);

        return Json(new { count = olastaMed });
    }


    //Post-metod som markerar ett meddelande (det meddelande-id vi anger) som läst om meddelandet inte är null
    //Sätter Read till true
    [HttpPost]
    [Authorize]
    public IActionResult MarkeraLast(int meddelandeId)
    {
        var meddelande = _testDb.Meddelande.FirstOrDefault(m => m.Mid == meddelandeId);
    if (meddelande != null)
    {
        meddelande.Last = true;
        _testDb.SaveChanges();
        return RedirectToAction("Inkorg"); // Om åtgärden lyckas, omdirigerar vi till Inkorg.
    }
    return NotFound(); // Om meddelandet inte hittas
    }
}




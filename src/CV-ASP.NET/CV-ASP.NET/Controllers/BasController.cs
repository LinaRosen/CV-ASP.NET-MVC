//using CV_ASP.NET.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using System.Security.Claims;

//namespace CV_ASP.NET.Controllers
//{
//    public abstract class BasController : Controller
//    {
//        protected string? HamtaAnv()
//        {
//            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//        }
//    }
//}

using CV_ASP.NET.DataContext;
using CV_ASP.NET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

namespace CV_ASP.NET.Controllers
{
    public abstract class BasController : Controller
    {
        private readonly TestDataContext _dbContext;

        // Korrekt namn på konstruktören
        public BasController(TestDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Hjälpmetod för att hämta användarens ID
        protected string? HamtaAnv()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        // Override för att hämta olästa meddelanden
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Kontrollera om användaren är inloggad
            if (User.Identity?.IsAuthenticated == true)
            {
                var userName = User.Identity.Name;

                if (!string.IsNullOrEmpty(userName))
                {
                    var user = _dbContext.Users.SingleOrDefault(u => u.UserName == userName);

                    if (user != null)
                    {
                        var antalOlastaMeddelanden = _dbContext.Meddelande
                            .Where(m => m.TillAnvandareId == user.Id && !m.Last)


                            .Count();

                        // Lägg till i ViewData
                        ViewData["OlastaMeddelanden"] = antalOlastaMeddelanden;
                    }
                }
            }

            base.OnActionExecuting(context);
        }
    }
}


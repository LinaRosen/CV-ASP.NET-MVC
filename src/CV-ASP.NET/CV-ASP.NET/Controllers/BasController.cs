using CV_ASP.NET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace CV_ASP.NET.Controllers
{
    public abstract class BasController : Controller
    {
        protected string? HamtaAnv()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}



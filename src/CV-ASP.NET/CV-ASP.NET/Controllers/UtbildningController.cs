using CV_ASP.NET.DataContext;
using CV_ASP.NET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CV_ASP.NET.Controllers
{
    public class UtbildningController : BasController
    {
        private TestDataContext testDb;

       
        public UtbildningController(TestDataContext testDb)
        {

            this.testDb = testDb;
            
        }

        [Authorize]
        [HttpGet]
        public IActionResult LaggTillUtb()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> LaggTillUtb(Utbildning utb)
        {
            return View(utb);
        }
    }
}

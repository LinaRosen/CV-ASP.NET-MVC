using CV_ASP.NET.DataContext;
using CV_ASP.NET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CV_ASP.NET.Controllers
{
    public class KompetensController : BasController
    {
        private TestDataContext testDb;


        public KompetensController(TestDataContext testDb)
        {

            this.testDb = testDb;

        }

        [Authorize]
        [HttpGet]
        public IActionResult LaggTillKomp()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> LaggTillKomp(Kompetenser komp)
        {
            return View(komp);
        }
    }
}

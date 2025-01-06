using CV_ASP.NET.DataContext;
using CV_ASP.NET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CV_ASP.NET.Controllers
{
    public class ErfarenhetController : BasController
    {
        private TestDataContext testDb;

        public ErfarenhetController(TestDataContext testDb)
        {

            this.testDb = testDb;

        }

        [Authorize]
        [HttpGet]
        public IActionResult LaggTillErf()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> LaggTillErf(Erfarenhet erf)
        {
            return View(erf);
        }
    }
}

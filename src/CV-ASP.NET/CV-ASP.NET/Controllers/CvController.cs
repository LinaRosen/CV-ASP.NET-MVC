using CV_ASP.NET.DataContext;
using CV_ASP.NET.Models;
using CV_ASP.NET.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CV_ASP.NET.Controllers
{
    public class CvController : BasController
    {
        private TestDataContext testDb;
        private readonly string _bildsökväg = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CvController(TestDataContext testDb, IWebHostEnvironment webHostEnvironment)
        {
            this.testDb = testDb;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> TaBortAllaCvn()
        {
            // Hämta alla CV:n från databasen
            var allaCvn = testDb.CV.ToList();

            // Ta bort alla CV:n
            testDb.CV.RemoveRange(allaCvn);
            await testDb.SaveChangesAsync(); // Spara ändringarna i databasen

            // Återgå till en annan vy eller huvudvyn
            return RedirectToAction("Anvsida", "AnvSida");
        }


        public async Task<IActionResult> VisaCv(string anvId)
        {
            return await VisaCvP(anvId);
        }

        [HttpPost]
        [ActionName("VisaCv")]
        public async Task<IActionResult> VisaCvP(string anvId)
        {
            VisaCvViewModel vm = new VisaCvViewModel { };
            string? inloggadAnv = base.HamtaAnv();
          

            // Hämta användare och CV
            vm.anvandare = testDb.Anvandare.Where(u => u.Id.Equals(inloggadAnv)).Single();
            vm.Cv = testDb.CV.Where(c => c.AnvandarNamn.Equals(inloggadAnv)).Single();

            // Om det inte är inloggad användare, uppdatera besökarantalet
            if (anvId != null && !anvId.Equals(inloggadAnv))
            {
                vm.Cv.AntalBesokare++;
                testDb.Update(vm.Cv);
                await testDb.SaveChangesAsync();
            }

            //vm.projekt = testDb.AnvProjekt
            //.Where(ap => ap.Anvid == inloggadAnv)
            //.Select(ap => testDb.Projekt.Single(p => p.Pid == ap.Pid))
            //.ToList();

            vm.erfarenhet = (IEnumerable<Models.Erfarenhet>)testDb.CV_Erfarenhet
                .Where(ce => ce.Cvid == vm.Cv.Cvid)
                .Select(ce => testDb.Erfarenhet.Single(e => e.Eid == ce.Eid))
                .ToList();

            vm.kompetenser = (IEnumerable<Models.Kompetenser>)testDb.CV_Kompetenser
                .Where(ck => ck.Cvid == vm.Cv.Cvid)
                .Select(ck => testDb.Kompetenser.Single(k => k.Kid == ck.Kid))
                .ToList();

            vm.utbildning = (IEnumerable<Models.Utbildning>)testDb.CV_Utbildning
                .Where(cu => cu.CVid == vm.Cv.Cvid)
                .Select(cu => testDb.Utbildning.Single(U => U.Uid == cu.Uid))
                .ToList();
            return View(vm);
        }

        [Authorize]

        [HttpGet]
        public IActionResult SkapaCv()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> skapaCv(CV scv)
        {
            if (ModelState.IsValid)
            {

                string? inloggadAnv = base.HamtaAnv();
                string fil = LaddaUppProfilbild(scv);
                scv.Profilbild= fil;
                scv.AnvandarNamn = inloggadAnv;

                
                
                await testDb.AddAsync(scv);
                await testDb.SaveChangesAsync();

                return RedirectToAction("Anvsida", "AnvSida");
            }
            return View(scv);
        }

        [Authorize]
        private string LaddaUppProfilbild(CV cvm)
        {
            string fileName = null;
            if (cvm.Bildfil != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");

                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                fileName = Guid.NewGuid().ToString() + "-" + cvm.Bildfil.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    cvm.Bildfil.CopyTo(fileStream);
                }
            }
            return fileName;
        }


    }
}
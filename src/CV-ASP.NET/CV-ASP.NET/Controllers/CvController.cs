using AspNetCoreGeneratedDocument;
using CV_ASP.NET.DataContext;
using CV_ASP.NET.Models;
using CV_ASP.NET.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace CV_ASP.NET.Controllers
{
    public class CvController : BasController
    {
        private TestDataContext testDb;
        
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

            

            if 
                (anvId == null || anvId.Equals(inloggadAnv))
            {

                vm.anvandare = testDb.Users.Where(a => a.Id.Equals(inloggadAnv)).FirstOrDefault();
                vm.Cv = testDb.CV.Where(c => c.AnvandarNamn.Equals(inloggadAnv)).FirstOrDefault();
            }
            // Om det inte är inloggad användare, uppdatera besökarantalet
            else
                
            {
                vm.anvandare = testDb.Users.Where(a => a.Id == anvId).FirstOrDefault();
                vm.Cv = testDb.CV.Where(c => c.AnvandarNamn == anvId).FirstOrDefault();

                if (vm.Cv != null) // Kontrollera om vm.Cv inte är null
                {
                    var totalaBesokare = testDb.CV.Where(c => c.Cvid == vm.Cv.Cvid).FirstOrDefault().AntalBesokare;
                    CV cv = vm.Cv;
                    cv.AntalBesokare = totalaBesokare + 1;
                    testDb.Update(cv);
                    await testDb.SaveChangesAsync();
                }

            }

            //vm.projekt = testDb.AnvProjekt
            //.Where(ap => ap.Anvid == inloggadAnv)
            //.Select(ap => testDb.Projekt.Single(p => p.Pid == ap.Pid))
            //.ToList();

            if (vm.Cv != null)
            {
                vm.erfarenheter = testDb.CV_Erfarenhet
                .Where(ce => ce.Cvid == vm.Cv.Cvid)
                .Join(testDb.Erfarenhet,
                 ce => ce.Eid,
                 e => e.Eid,
                 (ce, e) => new ErfarenhetViewModel
                {
                    Eid = e.Eid, // Mappa Eid från Erfarenhet till ViewModel
                    Titel = e.Titel,
                    Arbetsgivare = e.Arbetsgivare,
                    Beskrivning = e.Beskrivning,
                    StartDatum = ce.Startdatum,
                    Slutdatum = ce.Slutdatum
                })
               .ToList();

                vm.kompetenser = testDb.CV_Kompetenser
                 .Where(ce => ce.Cvid == vm.Cv.Cvid)
                .Join(testDb.Kompetenser,
                 ce => ce.Kid,
                k => k.Kid,
                 (ce, k) => new KompetensViewModel
                 {
                     kid = k.Kid,
                     KompetensNamn = ce.KompetensNamn,
                     Beskrivning = k.Beskrivning

                 })
                .ToList();

                vm.utbildningar = testDb.CV_Utbildning
                 .Where(cu => cu.CVid == vm.Cv.Cvid)
                .Join(testDb.Utbildning,
                 cu => cu.Uid,
                u => u.Uid,
                 (cu, u) => new UtbildningViewModel
                 {
                     uid = u.Uid,
                     Instutition = u.Instutition,
                     Kurs_program = u.Kurs_program,
                     Beskrivning = u.Beskrivning,
                     StartDatum = cu.Startdatum,
                     SlutDatum = cu.Slutdatum
                 })
                .ToList();
                return View(vm);

            }
            else
            {
                TempData["ErrorMessage"] = "Det finns inget CV för denna person.";

                // Omdirigera till startsidan
                return RedirectToAction("Index", "Sok");
            }


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
        public async Task<IActionResult> skapaCv(SkapaCvViewModel scv)
        {
            
            if (ModelState.IsValid)
            {
                string fil = LaddaUppProfilbild(scv);
                scv.cv.Profilbild = fil;

                string? inloggadAnv = base.HamtaAnv();
                scv.cv.AnvandarNamn = inloggadAnv;

                
                var cv = new CV();

                cv.Profilbild = fil;
                cv.Beskrivning = scv.cv.Beskrivning;
                cv.AnvandarNamn = base.HamtaAnv();

                await testDb.AddAsync(cv);
                await testDb.SaveChangesAsync();


                var erfarenhet = new Erfarenhet();

                erfarenhet.Arbetsgivare = scv.erfarenhet.Arbetsgivare;
                erfarenhet.Titel = scv.erfarenhet.Titel;
                erfarenhet.Beskrivning = scv.erfarenhet.Beskrivning;

                await testDb.Erfarenhet.AddAsync(erfarenhet);
                await testDb.SaveChangesAsync();

                int dettaCv = testDb.CV.Where(c => c.AnvandarNamn == inloggadAnv).Single().Cvid;

                var cvErfarenhet = new CV_Erfarenhet();

                cvErfarenhet.Cvid = dettaCv;
                cvErfarenhet.Startdatum = scv.cvErfarenhet.Startdatum;
                cvErfarenhet.Slutdatum = scv.cvErfarenhet.Slutdatum;
                cvErfarenhet.Eid = erfarenhet.Eid;
                
                await testDb.CV_Erfarenhet.AddAsync(cvErfarenhet);
                await testDb.SaveChangesAsync();


                var utbildning = new Utbildning();

             
                utbildning.Instutition = scv.utbildning.Instutition;
                utbildning.Beskrivning = scv.utbildning.Beskrivning;
                utbildning.Kurs_program = scv.utbildning.Kurs_program;

                await testDb.Utbildning.AddAsync(utbildning);
                await testDb.SaveChangesAsync();

                var cvUtbildning = new CV_Utbildning();
                cvUtbildning.CVid = dettaCv;
                cvUtbildning.Startdatum = scv.cvUtbildning.Startdatum;
                cvUtbildning.Slutdatum = scv.cvUtbildning.Slutdatum;
                cvUtbildning.Uid = utbildning.Uid;
                
                await testDb.CV_Utbildning.AddAsync(cvUtbildning);
                await testDb.SaveChangesAsync();


                var kompetenser = new Kompetenser();

                kompetenser.Beskrivning = scv.kompetenser.Beskrivning;
                

                await testDb.Kompetenser.AddAsync(kompetenser);
                await testDb.SaveChangesAsync();
               

                var cvKompetens = new CV_kompetenser();
                cvKompetens.Cvid = dettaCv;
                cvKompetens.Kid = kompetenser.Kid;
                cvKompetens.KompetensNamn = scv.cvKompetenser.KompetensNamn;




                await testDb.CV_Kompetenser.AddAsync(cvKompetens);
                await testDb.SaveChangesAsync();

                //cv.CvKompetenser = new List<CV_kompetenser> { cvKompetens };
                //cv.CvErfarenhet = new List<CV_Erfarenhet> { cvErfarenhet };
                //cv.CvUtbildning = new List<CV_Utbildning> { cvUtbildning };


                return RedirectToAction("Anvsida", "AnvSida");
            }
            return View(scv);
        }

        [Authorize]
        private string LaddaUppProfilbild(SkapaCvViewModel cvm)
        {
            string fileName = null;
            if (cvm.cv.Bildfil != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");

                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                fileName = Guid.NewGuid().ToString() + "-" + cvm.cv.Bildfil.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    cvm.cv.Bildfil.CopyTo(fileStream);
                }
            }
            return fileName;
        }

        [Authorize]
        [HttpGet]
        public IActionResult RedigeraCv()
        {
            try
            {
                string inloggadAnv = base.HamtaAnv();

                // Hämta användarens CV från databasen
                var dettaCv = testDb.CV.SingleOrDefault(c => c.AnvandarNamn == inloggadAnv);

                // Skapa ViewModel med data från användarens CV
                var model = new RedigeraCvViewModel
                {
                    Beskrivning = dettaCv.Beskrivning,
                    Profilbild = dettaCv.Profilbild
                };

                return View(model);
            }
            catch (Exception ex)
            {
                // Logga felet och visa ett felmeddelande
                
                TempData["ErrorMessage"] = "Ett fel uppstod. Försök igen.";
                return RedirectToAction("Anvsida", "AnvSida");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RedigeraCv(RedigeraCvViewModel model)
        {
                string inloggadAnv = base.HamtaAnv();
            try
            {
                var dettaCv = testDb.CV.SingleOrDefault(c => c.AnvandarNamn == inloggadAnv);

                string nyBild = LaddaUppProfilbild(new SkapaCvViewModel
                {
                    cv = new CV
                    {
                        Bildfil = model.BildFil
                    }

                });
                if (!string.IsNullOrEmpty(nyBild))
                {
                    dettaCv.Profilbild = nyBild; // Uppdatera profilbilden
                }
                
                dettaCv.Beskrivning = model.Beskrivning;
                
                testDb.Update(dettaCv);
                await testDb.SaveChangesAsync();

                //Om allt går bra omdirigeras användaren till VisaCv
                TempData["SuccessMessage"] = "CV har uppdaterats!";
                return RedirectToAction("VisaCv", new { anvId = inloggadAnv }); // Omdirigera till VisaCv
            }
            catch (Exception ex)
            {
                //Vid fel returneras vyn med samma data för att låta användaren försöka igen
                return View(model);
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult LaggTillErf()
        {

            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> LaggTillErf(ErfarenhetViewModel model)
        {
            string inloggadAnv = base.HamtaAnv();
            var erfarenhet = new Erfarenhet();

            erfarenhet.Eid= model.Eid;
            erfarenhet.Arbetsgivare = model.Arbetsgivare;
            erfarenhet.Titel = model.Titel;
            erfarenhet.Beskrivning = model.Beskrivning;
           

            await testDb.Erfarenhet.AddAsync(erfarenhet);
            await testDb.SaveChangesAsync();
            

            int dettaCv = testDb.CV.Where(c => c.AnvandarNamn == inloggadAnv).Single().Cvid;

            var cvErfarenhet = new CV_Erfarenhet();

            cvErfarenhet.Cvid = dettaCv;
            cvErfarenhet.Startdatum = (DateOnly)model.StartDatum;
            cvErfarenhet.Slutdatum = model.Slutdatum;
            cvErfarenhet.Eid = erfarenhet.Eid;

            await testDb.CV_Erfarenhet.AddAsync(cvErfarenhet);
            await testDb.SaveChangesAsync();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> TaBortErf(int id)
        {
            Models.Erfarenhet erf = testDb.Erfarenhet.Find(id);
            //Tar bort erfarentets-sobjektet från databasen och sparar ändringarna i databasen
            testDb.Erfarenhet.Remove(erf);
            await testDb.SaveChangesAsync();
            return RedirectToAction("VisaCv", "Cv");

        }


        [Authorize]
        [HttpGet]
        public IActionResult LaggTillUtb()
        {

            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> LaggTillUtb(UtbildningViewModel model)
        {
            string inloggadAnv = base.HamtaAnv();
            var utbildning = new Utbildning();

            utbildning.Uid = model.uid;
            utbildning.Kurs_program = model.Kurs_program;
            utbildning.Instutition = model.Instutition;
            utbildning.Beskrivning = model.Beskrivning;


            await testDb.Utbildning.AddAsync(utbildning);
            await testDb.SaveChangesAsync();


            int dettaCv = testDb.CV.Where(c => c.AnvandarNamn == inloggadAnv).Single().Cvid;

            var cvUtbildning = new CV_Utbildning();

            cvUtbildning.CVid = dettaCv;
            cvUtbildning.Startdatum = (DateOnly)model.StartDatum;
            cvUtbildning.Slutdatum = model.SlutDatum;
            cvUtbildning.Uid = utbildning.Uid;

            await testDb.CV_Utbildning.AddAsync(cvUtbildning);
            await testDb.SaveChangesAsync();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> TaBortUtb(int id)
        {
            Models.Utbildning utb = testDb.Utbildning.Find(id);
            //Tar bort erfarentets-sobjektet från databasen och sparar ändringarna i databasen
            testDb.Utbildning.Remove(utb);
            await testDb.SaveChangesAsync();
            return RedirectToAction("VisaCv", "Cv");

        }

        [Authorize]
        [HttpGet]
        public IActionResult LaggTillKomp()
        {

            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> LaggTillKomp(KompetensViewModel model)
        {
            string inloggadAnv = base.HamtaAnv();
            var kompetens = new Kompetenser();

            kompetens.Kid = model.kid;
            kompetens.Beskrivning = model.Beskrivning;
           
            await testDb.Kompetenser.AddAsync(kompetens);
            await testDb.SaveChangesAsync();

            int dettaCv = testDb.CV.Where(c => c.AnvandarNamn == inloggadAnv).Single().Cvid;

            var cV_Kompetenser = new CV_kompetenser();

            cV_Kompetenser.Cvid = dettaCv;
            cV_Kompetenser.KompetensNamn = model.KompetensNamn;
            cV_Kompetenser.Kid = kompetens.Kid;

            await testDb.CV_Kompetenser.AddAsync(cV_Kompetenser);
            await testDb.SaveChangesAsync();
            return RedirectToAction("VisaCv", "Cv");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> TaBortKomp(int id)
        {
            Models.Kompetenser komp = testDb.kompetensers.Find(id);
            //Tar bort erfarentets-sobjektet från databasen och sparar ändringarna i databasen
            testDb.Kompetenser.Remove(komp);
            await testDb.SaveChangesAsync();
            return RedirectToAction("VisaCv", "Cv");

        }
    }



}
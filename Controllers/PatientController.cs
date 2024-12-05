using HMS.Data;
using HMS.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HMS.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        private readonly AppDbContext _context;

        public PatientController(AppDbContext context)
        {
            _context = context;
        }
        private int GetLoggedInUserId()
        {
            var claimIdentity = User.Identity as ClaimsIdentity;
            return Convert.ToInt32(claimIdentity?.FindFirst(ClaimTypes.Sid)?.Value);
        }

         
        public IActionResult Index()
        {
            int userId = GetLoggedInUserId();
            var user = _context.Users.Find(userId);
            if (user == null) return NotFound();
            ViewBag.user = user;

            var AllPateint = _context.Patients.Where(x => x.ClearedBills == false).ToList();
            return View(AllPateint);
        }

        public IActionResult CheckedPatients()
        {
            int userId = GetLoggedInUserId();
            var user = _context.Users.Find(userId);
            if (user == null) return NotFound();
            ViewBag.user = user;

            var AllPateint = _context.Patients.Where(x => x.ClearedBills == true).ToList();
            return View(AllPateint);
        }


        public IActionResult AddNew(int? id)
        {
            if (id != null)
            {
                var PatientInDb = _context.Patients.SingleOrDefault(X => X.Id == id);
                return View(PatientInDb);
            }
            return View();
        }


         
        public IActionResult AddNewForm(Patient model)
        {
            if (model.Id == 0)
            {
                _context.Patients.Add(model);
            }
            else
            {
                _context.Patients.Update(model);
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

         
        public IActionResult Delete(int ID)
        {
            var PatientInDb = _context.Patients.SingleOrDefault(x => x.Id == ID);
            if (PatientInDb == null)
            {
                return NotFound();
            }
            _context.Patients.Remove(PatientInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int Id)
        {
            var Patient = _context.Patients.Find(Id);
            var Appoitments = _context.Appointments.Where(x => x.PatientId == Patient.Id).ToList();
            var DispencedMedicines = _context.DispensedMedicines.Where(x => x.PatientId == Patient.Id).ToList();
            var TestsResults = _context.LabResults.Where(x => x.PatientId == Patient.Id).ToList();
            var Bills = _context.Bills.Where(x => x.PatientId == Patient.Id).ToList();


            ViewBag.Patient = Patient;
            ViewBag.Appoitments = Appoitments;
            ViewBag.DispencedMedicines = DispencedMedicines;
            ViewBag.TestsResults = TestsResults;
            ViewBag.Bills = Bills;

            return View();
        }
    }
}

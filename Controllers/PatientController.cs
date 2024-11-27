using HMS.Data;
using HMS.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HMS.Controllers
{
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

        [Authorize]
        public IActionResult Index()
        {
            int userId = GetLoggedInUserId();
            var user = _context.Users.Find(userId);
            if (user == null) return NotFound();
            ViewBag.user = user;

            var AllPateint = _context.Patients.ToList();
            return View(AllPateint);
        }

        [Authorize]
        public IActionResult AddNew(int? id)
        {
            if (id != null)
            {
                var PatientInDb = _context.Patients.SingleOrDefault(X => X.Id == id);
                return View(PatientInDb);
            }
            return View();
        }


        [Authorize]
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

        [Authorize]
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

        [Authorize]
        public IActionResult CheckOut(int ID)
        {
            return View();
        }
    }
}

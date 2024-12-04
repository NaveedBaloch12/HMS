using HMS.Data;
using HMS.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    [Authorize]
    public class LabTechnicianController : Controller
    {
        private readonly AppDbContext _context;

        public LabTechnicianController(AppDbContext context)
        {
            _context = context;
        }

        // View pending test requests
        public IActionResult Index()
        {
            var testRequests = _context.DoctorSuggestions
                .Where(s => !s.IsCompleted)
                .Include(s => s.Patient)
                .Include(s => s.Doctor)
                .ToList();

            return View(testRequests);
        }

        // Mark test as performed and add result
        [HttpGet]
        public IActionResult PerformTest(int id)
        {
            var suggestion = _context.DoctorSuggestions
                .Include(s => s.Patient)
                .FirstOrDefault(s => s.Id == id);

            if (suggestion == null)
                return NotFound();

            return View(new LabResult
            {
                PatientId = suggestion.PatientId,
                DoctorSuggestionId = suggestion.Id,
                TestName = suggestion.TestName,
            });
        }

        [HttpPost]
        public IActionResult PerformTest(LabResult result)
        {
            if (ModelState.IsValid)
            {
                _context.LabResults.Add(result);

                var suggestion = _context.DoctorSuggestions.FirstOrDefault(s => s.Id == result.DoctorSuggestionId);
                if (suggestion != null)
                {
                    suggestion.IsCompleted = true;
                }

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(result);
        }

        // View completed tests
        public IActionResult CompletedTests()
        {
            var results = _context.LabResults
                .Include(r => r.Patient)
                .ToList();

            return View(results);
        }
    }

}

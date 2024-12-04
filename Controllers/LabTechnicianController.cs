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
            var testRequests = _context.SuggestedTests
                .Where(x => x.IsCompleted != true)
                .Include(s => s.Patient)
                .Include(s => s.Doctor)

                .ToList();

            return View(testRequests);
        }

        // Mark test as performed and add result
        public IActionResult PerformTest(int id)
        {
            var suggestion = _context.SuggestedTests
                .Include(s => s.Patient)
                .FirstOrDefault(s => s.Id == id);

            if (suggestion == null)
                return NotFound();

            if (suggestion.PatientId == null || !_context.Patients.Any(p => p.Id == suggestion.PatientId))
            {
                ModelState.AddModelError("", "Invalid Patient ID.");
                return View("Error"); // Or redirect to an error view
            }

            return View(new LabResult
            {
                PatientId = suggestion.PatientId,
                DoctorSuggestionId = suggestion.Id,
                TestName = suggestion.TestName,
            });
        }


        [HttpPost]
        public IActionResult PerformTest(int patientId, int DoctorSuggestionId, string testName, string Result)
        {
            if (ModelState.IsValid)
            {
                LabResult result = new LabResult 
                {
                    PatientId = patientId,
                    TestName = testName,
                    DoctorSuggestionId = DoctorSuggestionId,
                    Result = Result,
                    PerformedDate = DateTime.Now

                };
                _context.LabResults.Add(result);

                // Mark suggestion as completed
                var suggestion = _context.SuggestedTests.FirstOrDefault(s => s.Id == DoctorSuggestionId);
                if (suggestion != null)
                {
                    suggestion.IsCompleted = true;
                }

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
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

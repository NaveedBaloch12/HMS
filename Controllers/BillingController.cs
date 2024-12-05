using HMS.Data;
using HMS.Entites;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class BillingController : Controller
    {

        private readonly AppDbContext _context;

        public BillingController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GenerateBill(int patientId)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.Id == patientId);
            if (patient == null) return NotFound("Patient Not Found");

            // Calculate MedicineCost and TestCost
            var medicines = _context.DispensedMedicines.Where(m => m.PatientId == patientId);
            decimal medicineCost = medicines.Sum(m => m.TotalPrice);
            var testCost = _context.LabResults.Where(t => t.PatientId == patientId).Sum(t => t.Cost);

            var model = new BillingViewModel
            {
                PatientId = patient.Id,
                PatientName = patient.Name,
                DoctorFee = Globals.DoctorFee,
                MedicineCost = medicineCost,
                TestCost = testCost
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult SaveBill(BillingViewModel model)
        {
            var bill = new Bill
            {
                PatientId = model.PatientId,
                DoctorFee = model.DoctorFee,
                MedicineCost = model.MedicineCost,
                TestCost = model.TestCost,
                TotalAmount = model.TotalAmount,
                Date = DateTime.Now
            };

            var Patient = _context.Patients.Find(model.PatientId);
            Patient.ClearedBills = true;

            _context.Patients.Update(Patient);
            _context.Bills.Add(bill);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");

        }

    }
}

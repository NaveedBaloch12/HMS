using HMS.Data;
using HMS.Entites;
using HMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HMS.Controllers
{
    public class PharmacistController : Controller
    {
        private readonly AppDbContext _context;

        public PharmacistController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            var patients = _context.Patients
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Gender,
                    p.Contact
                })
                .ToList();

            return View(patients);
        }

        [HttpPost]
        [HttpPost]
        public IActionResult SearchPatients([FromBody] string searchTerm)
        {
            var patients = _context.Patients
                .Where(p => string.IsNullOrEmpty(searchTerm) ||
                            p.Name.Contains(searchTerm))
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Gender,
                    p.Contact
                })
                .ToList();

            return Json(patients);
        }


        // Action to display available medicines
        [Authorize]
        public IActionResult Inventory()
        {
            var medicines = _context.PharmacyInventories.ToList();
            return View(medicines);
        }

        // Action to display form for dispensing medicines
        [Authorize]
        public IActionResult DispenseMedicine(int examinationId)
        {
            var examination = _context.Examinations.Include(e => e.Medicines).FirstOrDefault(e => e.Id == examinationId);
            if (examination == null) return NotFound();

            var patient = _context.Patients.FirstOrDefault(p => p.Id == examination.PatientId);
            if (patient == null) return NotFound();

            var inventory = _context.PharmacyInventories.ToList();

            var model = new DispenseMedicineViewModel
            {
                Patient = patient,
                ExaminationId = examinationId,
                Medicines = examination.Medicines.ToList(),
                Inventory = inventory
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Dispense(DispenseMedicineViewModel model, bool[] dispensed)
        {
            if (dispensed != null && model.Medicines != null)
            {
                for (int i = 0; i < model.Medicines.Count; i++)
                {
                    if (dispensed[i])
                    {
                        var medicine = model.Medicines[i];
                        var inventoryItem = _context.PharmacyInventories.FirstOrDefault(inv => inv.MedicineName == medicine.Name);
                        if (inventoryItem != null && inventoryItem.StockQuantity > 0)
                        {
                            inventoryItem.StockQuantity--;
                        }
                    }
                }
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Pharmacy");
        }

        [Authorize]
        public IActionResult AddNew(int? id)
        {
            if (id != null)
            {
                var PatientInDb = _context.PharmacyInventories.SingleOrDefault(X => X.Id == id);
                return View(PatientInDb);
            }
            return View();
        }


        [Authorize]
        public IActionResult AddNewForm(PharmacyInventory model)
        {
            if (model.Id == 0)
            {
                _context.PharmacyInventories.Add(model);
            }
            else
            {
                _context.PharmacyInventories.Update(model);
            }

            _context.SaveChanges();
            return RedirectToAction("Inventory");
        }

        [Authorize]
        public IActionResult Delete(int ID)
        {
            var MedicineinDb = _context.PharmacyInventories.SingleOrDefault(x => x.Id == ID);
            if (MedicineinDb == null)
            {
                return NotFound();
            }
            _context.PharmacyInventories.Remove(MedicineinDb);
            _context.SaveChanges();
            return RedirectToAction("Inventory");
        }
    }
}

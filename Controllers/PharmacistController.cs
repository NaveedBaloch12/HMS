using HMS.Classes;
using HMS.Data;
using HMS.Entites;
using HMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HMS.Controllers
{
    [Authorize]
    public class PharmacistController : Controller
    {
        private readonly AppDbContext _context;

        public PharmacistController(AppDbContext context)
        {
            _context = context;
        }


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
        public IActionResult Inventory()
        {
            var medicines = _context.PharmacyInventories.ToList();
            return View(medicines);
        }

        // Action to display form for dispensing medicines
        public IActionResult DispenseMedicine(int id)
        {
            Globals.CurrentExaminationId = id;
            Globals.CartMedicinesList.Clear();
            Globals.TotalPrice = 0;

            var examination = _context.Examinations.Include(e => e.Medicines).FirstOrDefault(e => e.Id == id);
            if (examination == null) return NotFound();

            var patient = _context.Patients.FirstOrDefault(p => p.Id == examination.PatientId);
            if (patient == null) return NotFound();

            var model = new DispenseMedicineViewModel
            {
                Patient = patient,
                ExaminationId = id,
                Medicines = examination.Medicines.ToList(),
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AddMedicine(string name, int quantity, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name) || quantity <= 0 || price <= 0)
            {
                return BadRequest("Invalid input.");
            }

            Globals.CartMedicinesList.Add(new CartMedicine
            {
                ID = Globals.CartMedicinesList.Count + 1,
                Name = name,
                Quantity = quantity,
                Price = price
            });

            Globals.TotalPrice = Globals.CartMedicinesList.Sum(m => m.Price * m.Quantity);

            return PartialView("_MedicineListPartial", Globals.CartMedicinesList);
        }

        [HttpPost]
        public IActionResult DeleteMedicine(int id)
        {
            var medicine = Globals.CartMedicinesList.FirstOrDefault(m => m.ID == id);
            if (medicine != null)
            {
                Globals.CartMedicinesList.Remove(medicine);
                Globals.TotalPrice = Globals.CartMedicinesList.Sum(m => m.Price * m.Quantity);
                return RedirectToAction("DispenseMedicine", new { id = Globals.CurrentExaminationId});
            }
            return NotFound("Medicine not found.");
        }


        public IActionResult AddNew(int? id)
        {
            if (id != null)
            {
                var PatientInDb = _context.PharmacyInventories.SingleOrDefault(X => X.Id == id);
                return View(PatientInDb);
            }
            return View();
        }



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
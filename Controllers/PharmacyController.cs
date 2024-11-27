using HMS.Data;
using HMS.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class PharmacyController : Controller
    {
        private readonly AppDbContext _context;

        public PharmacyController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            var AllMed = _context.Medicines.ToList();

            float TotalBill = 0;
            foreach (var item in AllMed) 
            {
                TotalBill += item.Price;
            }

            ViewBag.TotalBill = TotalBill;
            return View(AllMed);
        }

        [Authorize]
        public IActionResult AddNew(int? id)
        {
            if (id != null)
            {
                var MedInDb = _context.Medicines.SingleOrDefault(X => X.Id == id);
                return View(MedInDb);
            }
            return View();
        }


        [Authorize]
        public IActionResult AddNewForm(Medicine model)
        {
            if (model.Id == 0)
            {
                _context.Medicines.Add(model);
            }
            else
            {
                _context.Medicines.Update(model);
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Delete(int ID)
        {
            var MedInDb = _context.Medicines.SingleOrDefault(x => x.Id == ID);
            if (MedInDb == null)
            {
                return NotFound();
            }
            _context.Medicines.Remove(MedInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

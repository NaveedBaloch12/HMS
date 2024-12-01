using HMS.Data;
using HMS.Entites;
using HMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class ExamineController : Controller
    {
        private readonly AppDbContext _context;

        public ExamineController(AppDbContext Context) 
        {
            _context = Context;
        }

        [Authorize]
        public IActionResult Index(int Id)
        {
            var Appointment = _context.Appointments.Find(Id);
            if (Appointment == null)
            {
                return NotFound();
            }

            var Patient = _context.Patients.Find(Appointment.PatientId);
            if (Patient == null)
            {
                return NotFound();
            }

            ExamineIndexViewModel ViewModel = new ExamineIndexViewModel
            {
                Patient = Patient,
                Appointment = Appointment
            };
            
            return View(ViewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult SaveExamination(ExamineIndexViewModel model)
        {
            // Debug to verify data passed from the form
            if (model.Examination == null || model.Examination.AppointmentId == 0)
            {
                return BadRequest("Examination data is missing.");
            }

            var appointment = _context.Appointments.Find(model.Examination.AppointmentId);
            if (appointment == null)
            {
                return NotFound("Appointment not found.");
            }

            appointment.Status = "Checked";

            // Process the examination
            var examination = new Examination
            {
                AppointmentId = model.Examination.AppointmentId,
                Diagnosis = model.Examination.Diagnosis,
                Notes = model.Examination.Notes,
                PatientId = appointment.PatientId,
                Medicines = model.Examination.Medicines
            };

            _context.Examinations.Add(examination);
            _context.Appointments.Update(appointment);
            _context.SaveChanges();

            return RedirectToAction("Index", "Examine");
        }


        [Authorize]
        public IActionResult ViewPrescription(int id)
        {
            var Appointment = _context.Appointments.Find(id);
            if (Appointment == null) return NotFound();

            var Patient = _context.Patients.Find(Appointment.PatientId);
            if (Patient == null) return NotFound();

            var Examine = _context.Examinations.Where(x => x.AppointmentId == id).FirstOrDefault();

            if(Examine == null) return NotFound("No Medicine is Suggested to Patient..");

            var Medicines = _context.Medicines.Where(x => x.ExaminationId == Examine.Id);

            ViewBag.Medicines = Medicines;
            ViewBag.Patient = Patient;

            return View();
        }

    }
}

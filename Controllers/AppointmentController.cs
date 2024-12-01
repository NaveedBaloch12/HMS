using HMS.Data;
using HMS.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class AppointmentController : Controller
{
    private readonly AppDbContext _context;

    public AppointmentController(AppDbContext context)
    {
        _context = context;
    }

    [Authorize]
    public IActionResult Index()
    {
        var appointments = _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor) 
            .ToList();
        return View(appointments);
    }

    [Authorize]
    public IActionResult Create()
    {
        ViewBag.Patients = _context.Patients.ToList();
        ViewBag.Doctors = _context.Users.Where(u => u.Role == "Doctor").ToList();
        return View();
    }

    [Authorize]
    [HttpPost]
    public IActionResult Create(Appointment appointment)
    {
        if (ModelState.IsValid)
        {
            appointment.Status = "Waiting";
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        ViewBag.Patients = _context.Patients.ToList();
        ViewBag.Doctors = _context.Users.Where(u => u.Role == "Doctor").ToList();
        return View(appointment);
    }

    [Authorize]
    public IActionResult Delete(int id)
    {
        var appointment = _context.Appointments.Find(id);
        if (appointment == null) return NotFound();

        var relatedExaminations = _context.Examinations.Where(e => e.AppointmentId == id).ToList();
        if (relatedExaminations.Any())
        {
            _context.Examinations.RemoveRange(relatedExaminations);
        }



        _context.Appointments.Remove(appointment);
        _context.SaveChanges();

        return RedirectToAction(actionName: "Index", controllerName: "Appointment");
    }
}

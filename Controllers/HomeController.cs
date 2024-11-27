using HMS.Data;
using HMS.Entites;
using HMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace HMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
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

            var viewModel = new HomeIndexViewModel();
            viewModel.User = user;

            if (user.Role == "Doctor")
            {
                viewModel.Appointments = _context.Appointments
                                                 .Include(a => a.Patient)
                                                 .Where(a => a.DoctorId == user.Id && a.Status == "Waiting")
                                                 .ToList();
            }

            Globals.CountUsers = _context.Users.Count();
            Globals.CountPatients = _context.Patients.Count();

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

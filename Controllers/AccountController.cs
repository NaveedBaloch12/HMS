using HMS.Data;
using HMS.Entites;
using HMS.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HMS.Controllers
{
    public class AccountController : Controller
    {

        private readonly AppDbContext _context;
        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(LogInViewModel Model)
        {
            if (ModelState.IsValid)
            {
                var User = _context.Users.Where(x =>
                x.UserName == Model.UserName &&
                x.Password == Model.Password &&
                x.Role == Model.Role).FirstOrDefault();


                if (User != null)
                {
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name , User.Name),
                new Claim(ClaimTypes.Sid,Convert.ToString(User.Id)),
                new Claim(ClaimTypes.Email, User.Email),
                new Claim(ClaimTypes.Role, User.Role),
            };

                    var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));

                    return RedirectToAction(controllerName: "Home", actionName: "Index");
                }
                else
                {
                    ViewBag.Message = "Ooops! User Not Found in Our Database.";
                }
            }
            return View(Model);
        }

        [Authorize]
        public IActionResult Register()
        {
            return View();
        }

        [Authorize]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(controllerName: "Account", actionName: "LogIn");
        }

        [Authorize]
        public IActionResult Settings()
        {
            var claimIdentity = User.Identity as ClaimsIdentity;
            int UserId = Convert.ToInt32(claimIdentity?.FindFirst(ClaimTypes.Sid)?.Value);

            var user = _context.Users.FirstOrDefault(u => u.Id == UserId);

            if (user == null) return NotFound("User not found.");

            return View(user);
        }

        private int GetLoggedInUserId()
        {
            var claimIdentity = User.Identity as ClaimsIdentity;
            return Convert.ToInt32(claimIdentity?.FindFirst(ClaimTypes.Sid)?.Value);
        }

        // Edit Name
        [Authorize]
        public IActionResult EditName()
        {
            int userId = GetLoggedInUserId();
            var user = _context.Users.Find(userId);
            if (user == null) return NotFound();

            return View(user);
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditName(User updatedUser)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.Find(updatedUser.Id);
                if (user == null) return NotFound();

                user.Name = updatedUser.Name;
                _context.SaveChanges();
                return RedirectToAction("Settings");
            }

            return View(updatedUser);
        }

        // Edit Email
        [Authorize]
        public IActionResult EditEmail()
        {
            int userId = GetLoggedInUserId();
            var user = _context.Users.Find(userId);
            if (user == null) return NotFound();

            return View(user);
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditEmail(User updatedUser)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.Find(updatedUser.Id);
                if (user == null) return NotFound();

                user.Email = updatedUser.Email;
                _context.SaveChanges();
                return RedirectToAction("Settings");
            }

            return View(updatedUser);
        }

        // Edit Contact Number
        [Authorize]
        public IActionResult EditContact()
        {
            int userId = GetLoggedInUserId();
            var user = _context.Users.Find(userId);
            if (user == null) return NotFound();

            return View(user);
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditContact(User updatedUser)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.Find(updatedUser.Id);
                if (user == null) return NotFound();

                user.ContactNo = updatedUser.ContactNo;
                _context.SaveChanges();
                return RedirectToAction("Settings");
            }

            return View(updatedUser);
        }

        // Edit Password
        [Authorize]
        public IActionResult EditPassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditPassword(string currentPassword, string newPassword)
        {
            int userId = GetLoggedInUserId();
            var user = _context.Users.Find(userId);
            if (user == null) return NotFound();

            if (user.Password != currentPassword)
            {
                ModelState.AddModelError(string.Empty, "Current password is incorrect.");
                return View();
            }

            user.Password = newPassword;
            _context.SaveChanges();
            return RedirectToAction("Settings");
        }
    }

}

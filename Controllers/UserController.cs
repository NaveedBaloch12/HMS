using HMS.Data;
using HMS.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        private readonly EmailService _emailService;


        public UserController(AppDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        [Authorize]
        public IActionResult Index()
        {
            var AllUsers = _context.Users.ToList();
            return View(AllUsers);
        }

        [Authorize]
        public IActionResult AddNew(int? id)
        {
            if (id != null)
            {
                var UserInDb = _context.Users.SingleOrDefault(X => X.Id == id);
                return View(UserInDb);
            }
            return View();
        }

        [Authorize]
        public IActionResult AddNewForm(User model)
        {
            bool isNewUser = model.Id == 0;

            if (isNewUser)
            {
                // Adding a new user
                _context.Users.Add(model);

                // Send email notification for new users only
                if (isNewUser)
                {
                    try
                    {
                        var subject = "Welcome to HMS";
                        var body = $@"
                    Dear {model.Name},<br><br>
                    Your account has been created successfully.<br>
                    <strong>Username:</strong> {model.UserName}<br>
                    <strong>Password:</strong> {model.Password}<br><br>
                    Please access you account and change Password ASAP. <br><br>
                    Thank you,<br>
                    HMS Team";

                        // Send email using the EmailService
                        _emailService.SendEmailAsync(model.Email, subject, body).Wait();
                    }
                    catch (Exception ex)
                    {
                        // Log error (optional)
                        Console.WriteLine($"Email sending failed: {ex.Message}");
                    }
                }
            }
            else
            {
                // Updating an existing user
                _context.Users.Update(model);

                // Send email notification for new users only
                if (isNewUser)
                {
                    try
                    {
                        var subject = "Account Updation";
                        var body = $@"
                    Dear {model.Name},<br><br>
                    Your account has been Updated successfully.<br>
                    <strong>Username:</strong> {model.Email}<br>
                    <strong>Password:</strong> {model.Password}<br>
                    <strong>Role:</strong> {model.Role}<br><br>
                    Please access you account and change Password ASAP. <br><br>
                    Thank you,<br>
                    HMS Team";

                        // Send email using the EmailService
                        _emailService.SendEmailAsync(model.Email, subject, body).Wait();
                    }
                    catch (Exception ex)
                    {
                        // Log error (optional)
                        Console.WriteLine($"Email sending failed: {ex.Message}");
                    }
                }
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Delete(int ID)
        {
            var UserInDb = _context.Users.SingleOrDefault(x => x.Id == ID);
            if (UserInDb == null)
            {
                return NotFound();
            }

            if (UserInDb.UserName != "Admin@123")
            {
                _context.Users.Remove(UserInDb);
                _context.SaveChanges();

                try
                {
                    var subject = "Account Deletion";
                    var body = $@"
                    Dear {UserInDb.Name},<br><br>
                    Your account has been Deleted due to some reason. You will not able to access you account<br><br>
                    Thank you,<br>
                    HMS Team";

                    // Send email using the EmailService
                    _emailService.SendEmailAsync(UserInDb.Email, subject, body).Wait();
                }
                catch (Exception ex)
                {
                    // Log error (optional)
                    Console.WriteLine($"Email sending failed: {ex.Message}");
                }

            }
            return RedirectToAction("Index");
        }
    }
}

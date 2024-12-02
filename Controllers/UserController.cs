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
                var Subject = "Welcome to HMS";
                var Body = $@"Your account has been created successfully.<br>
                        <strong>Username:</strong> {model.UserName}<br>
                        <strong>Password:</strong> {model.Password}<br><br>
                        <strong>Link:</strong> <a href= '#' >HMS System</a> <br><br>
                        Please access you account and change Password ASAP. <br><br>";
                SendEmail(model, Subject, Body);
                try
                {
                    var subject = "Welcome to HMS";
                    var body = $@"
                        Dear {model.Name},<br><br>
                        c
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
            else
            {
                // Updating an existing user
                _context.Users.Update(model);

                // Send email notification for new users only
                var Subject = "Account Updation";
                var Body = $@"Your account has been Upddated successfully.<br>
                        <strong>Username:</strong> {model.UserName}<br>
                        <strong>Password:</strong> {model.Password}";
                SendEmail(model, Subject, Body);
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

                SendEmail(UserInDb, "Account Deletion", "Your account has been Deleted due to some reason. You will not able to access you account");

            }
            return RedirectToAction("Index");
        }


        // Function to  Send Email
        private void SendEmail(User user, string Prmsubject, string Prmbody)
        {
            try
            {
                var subject = Prmsubject;
                var body = $@"
                    Dear {user.Name},<br><br>
                    {Prmbody}<br><br>
                    Thank you,<br>
                    HMS Team";

                // Send email using the EmailService
                _emailService.SendEmailAsync(user.Email, subject, body).Wait();
            }
            catch (Exception ex)
            {
                // Log error (optional)
                Console.WriteLine($"Email sending failed: {ex.Message}");
            }
        }

    }
}

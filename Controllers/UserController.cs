using HMS.Data;
using HMS.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        

    public UserController(AppDbContext context)
        {
            _context = context;
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
            if (model.Id == 0)
            {
                _context.Users.Add(model);
            }
            else
            {
                _context.Users.Update(model);
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
            }
            return RedirectToAction("Index");
        }
    }
}

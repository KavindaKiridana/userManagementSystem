using _4.Models;
using _4.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace _4.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            HttpContext.Session.SetString("isLogged", "false");
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserLogin model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users
                    .FirstOrDefault(u => u.Name == model.Name);

                if (user != null && BCrypt.Net.BCrypt.EnhancedVerify(model.Password, user.Password))
                {
                    if (!user.IsDeleted)
                    {
                        // Successfully logged in
                        HttpContext.Session.SetString("isLogged", "true");
                        HttpContext.Session.SetString("userName", user.Name);
                        HttpContext.Session.SetString("isAdmin", user.IsAdmin.ToString());
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        // This is a deleted user
                        return RedirectToAction("Terminate", "Login");
                    }
                }
                else
                {
                    // Invalid login attempt
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.SetString("isLogged", "false");
            return RedirectToAction("Login", "Login");
        }

        public IActionResult Terminate()
        {
            return View();
        }
    }
}

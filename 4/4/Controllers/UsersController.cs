using _4.Models;
using _4.Services;
using Microsoft.AspNetCore.Mvc;

namespace _4.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment environment;

        public UsersController(ApplicationDbContext context, IWebHostEnvironment environment )
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var users = context.Users.ToList();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserDto userDto)
        {
            if (!ModelState.IsValid) 
            { 
                return View(userDto);
            }

            //save the user in the database
            User user = new User()
            {
                Name = userDto.Name,
                Password = userDto.Password,
                IsAdmin = userDto.IsAdmin,
                IsDeleted=false //need an attention to this line
            };

            context.Users.Add(user);
            context.SaveChanges();

            return RedirectToAction("Index", "Users");
        }

        public IActionResult Edit(int id) 
        { 
            var user = context.Users.Find(id);
            if (user == null) {
                return RedirectToAction("Index", "Users");
            }
            //create userDto from user
            var userDto = new UserDto()
            {
                Name = user.Name,
                IsAdmin = user.IsAdmin,
                IsDeleted=user.IsDeleted
            };
            ViewData["UserId"] = user.ID;
            return View(userDto);
        }

        [HttpPost]
        public IActionResult Edit(int id, UserDto userDto)
        {
            var user = context.Users.Find(id);
            if (user == null)
            {
                return RedirectToAction("Index", "Users");
            }
            if (!ModelState.IsValid)
            {
                ViewData["UserId"] = user.ID;
                return View(userDto);
            }

            user.Name = userDto.Name;
            user.IsAdmin = userDto.IsAdmin;
            context.SaveChanges();
            return RedirectToAction("Index", "Users");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var user = context.Users.Find(id);
            if (user == null)
            {
                return RedirectToAction("Index", "Users");
            }
            // Change user's IsDeleted status to true and save it
            user.IsDeleted = true;
            context.SaveChanges();

            return RedirectToAction("Index", "Users");
        }
    }
}

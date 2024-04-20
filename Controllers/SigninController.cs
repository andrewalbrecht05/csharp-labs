using Lab3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lab3.Controllers
{
    public class SigninController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(User user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (UserProvider.IsAuthorizedUser(user.Name, user.Password))
            {
                HttpContext.Session.SetString("Username", user.Name);
                Trace.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}: {user.Name} successfully authorized");
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("Password", "Помилка у логіні або паролі");
            return View();
        }
    }
}
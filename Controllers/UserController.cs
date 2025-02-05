using Forum_RP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Cryptography;

namespace Forum_RP.Controllers
{
    public class UserController : Controller
    {
        private readonly ForumContext _context;

        public UserController(ForumContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            ViewBag.CurrentUser = HttpContext.Session.GetString("UserName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User user, string ConfirmPassword)
        {
            ViewBag.CurrentUser = HttpContext.Session.GetString("UserName");

            if (user.Password != ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Nieporawne wpisanie haseł!");
            }
            if (ModelState.IsValid)
            {
                user.CreateDate = DateTime.Now;
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }

        public IActionResult Login()
        {
            ViewBag.CurrentUser = HttpContext.Session.GetString("UserName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == username && u.Password == password);
            if (user != null)
            {
                HttpContext.Session.SetInt32("UserID", user.UserId);
                HttpContext.Session.SetString("UserName", user.UserName);
                TempData["SuccessMessage"] = "Zalogowany!";
                return RedirectToAction("Index", "Home");
            }
            TempData["ErrorMessage"] = "Invalid username or password";
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            ViewBag.CurrentUser = HttpContext.Session.GetString("UserName");

            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(string currentPassword, string newPassword)
        {
            ViewBag.CurrentUser = HttpContext.Session.GetString("UserName");

            var userName = HttpContext.Session.GetString("UserName");
            if (userName == null)
            {
                return RedirectToAction("Login");
            }

            var user = _context.Users.FirstOrDefault(u => u.UserName == userName);
            if (user == null || currentPassword != user.Password)
            {
                TempData["ErrorMessage"] = "Niepoprawne hasło.";
                return View();
            }
            else
            {
                user.Password = newPassword;
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Hasło zmienione.";
            }

            return RedirectToAction("Index", "Home");
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace TeknikServisTanitim.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "1234")
            {
                HttpContext.Session.SetString("IsAdmin", "true");
                return RedirectToAction("List", "Request");
            }

            ViewBag.Hata = "Geçersiz giriş.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}

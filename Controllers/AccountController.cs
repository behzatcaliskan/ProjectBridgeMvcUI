using Microsoft.AspNetCore.Mvc;
using projectbridgemvc.Models;

namespace projectbridgemvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly MvcDbContext context;

        public AccountController(MvcDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new AppUser());
        }

        [HttpPost]
        public IActionResult Login(AppUser model)
        {
            var user = context.AppUsers
                .FirstOrDefault(x => x.UserName == model.UserName && x.Password == model.Password);

            if (user != null)
            {
                HttpContext.Session.SetString("FullName", user.FullName);
                HttpContext.Session.SetString("UserName", user.UserName);
                HttpContext.Session.SetString("Role", user.Role);

                if (user.Role == "Admin")
                {
                    return RedirectToAction("Index", "Products");
                }
                else
                {
                    return RedirectToAction("Index", "Dashboard"); 
                }
            }

            ViewBag.Error = "Kullanıcı adı veya şifre hatalı.";
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
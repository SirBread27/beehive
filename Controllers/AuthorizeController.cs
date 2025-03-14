using Beehive.Models;
using Beehive.Models.DbRecords;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace Beehive.Controllers
{
    public class AuthorizeController(ApplicationContext appContext) : Controller
    {
        readonly ApplicationContext db = appContext;

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(AuthModel am)
        {
            if (!am.ArePasswordsEqual || am.Name is null)
            {
                ViewBag.Message = "Пароли не совпадают";
                return View("Register");
            }
            if (db.Users.Any(u => u.Email == am.Email))
            {
                ViewBag.Message = "Данный e-mail занят";
                return View("Register");
            }
            var user = new UserRecord()
            {
                Id = Guid.NewGuid(),
                Name = am.Name,
                Email = am.Email,
                Password = am.PasswordHash
            };
            db.Users.Add(user);
            db.SaveChanges();
            Authorize(user);
            return new RedirectToActionResult("Search", "Direct", new { query = "" }); //STUB
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogUser(AuthModel am)
        {
            var user = db.Users.FirstOrDefault(u => u.Email == am.Email);
            if (user is null)
            {
                ViewBag.Message = "Пользователь не найден";
                return View("Login");
            }
            if (user.Password != am.PasswordHash)
            {
                ViewBag.Message = "Неверный пароль";
                return View("Login");
            }
            Authorize(user);
            return new RedirectToActionResult("Search", "Direct", new { query = "" }); //STUB
        }

        [Authorize]
        [HttpPut]
        public IActionResult LogOut()
        {
            var g = HttpContext.User.FindFirst(ClaimTypes.Name)!.Value;
            Models.User.Current.Remove(g);
            Models.User.CurrentRecord.Remove(g);
            HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        async void Authorize(UserRecord am) 
        {
            var guid = am.Id.ToString();
            var claims = new List<Claim> { new(ClaimTypes.Name, guid) };
            var id = new ClaimsIdentity(claims, "Cookies");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
            Models.User.Current[guid] = new Models.User(am);
            Models.User.CurrentRecord[guid] = am;
        }
    }
}

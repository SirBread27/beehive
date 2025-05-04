using Beehive.Models;
using Beehive.Models.DbRecords;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Cryptography;
using System.Security;
using System.Runtime.InteropServices;
using Humanizer.Bytes;

namespace Beehive.Controllers
{
    public class AuthorizeController(ApplicationContext appContext, IHubContext<DirectHub> hubContext) : Controller
    {
        readonly ApplicationContext db = appContext;
        readonly IHubContext<DirectHub> hub = hubContext;

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(RegisterModel am)
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
            var rsa = RSA.Create();
            var k = Rfc2898DeriveBytes.Pbkdf2(am.Password, am.Salt, 270000, HashAlgorithmName.SHA512, 64);
            var priv = rsa.ExportEncryptedPkcs8PrivateKey(k, new PbeParameters(PbeEncryptionAlgorithm.Aes256Cbc, 
                HashAlgorithmName.SHA512, 270000));
            var user = new UserRecord()
            {
                Id = Guid.NewGuid(),
                Name = am.Name,
                Email = am.Email,
                Salt = am.Salt,
                Pbkdf2 = am.Pbkdf2,
                PublicKey = rsa.ExportRSAPublicKey(),
                EncryptedPrivateKey = priv
            };
            rsa.Dispose();
            db.Users.Add(user);
            db.SaveChanges();
            GlobalVals.Passwords.Add(user.Id, k);
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
            if (user.Pbkdf2 != am.Pbkdf2)
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
            var claims = new List<Claim> { new(ClaimTypes.Name, guid), new(ClaimTypes.NameIdentifier, guid) };
            var id = new ClaimsIdentity(claims, "Cookies");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
            Models.User.Current[guid] = new User(am);
            Models.User.CurrentRecord[guid] = am;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Beehive.Models.PageModels;
using Beehive.Models;
using Microsoft.AspNetCore.Authorization;
using Beehive.Models.DbRecords;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Beehive.Controllers
{
    public class DirectController(ApplicationContext context) : Controller
    {
        readonly ApplicationContext db = context;
        private string Current => HttpContext.User.FindFirst(ClaimTypes.Name)!.Value;


        [HttpGet]
        [Authorize]
        public IActionResult Search(string? query)
        {
            //var user = HttpContext.User.FindFirst(ClaimTypes.Name)!.Value;
            if (Models.User.Current is null) return Unauthorized();
            return View(new UserSearchPageModel(db.Users, Guid.Parse(Current), query));
        }

        [HttpGet]
        [Authorize]
        public IActionResult Messages(Guid id)
        {
            if (Models.User.Current is null) return Unauthorized();
            var user = db.Users.FirstOrDefault(e => e.Id == id);
            if (user is null)
                return NotFound();
            var current = Models.User.Current[Current];
            return View(new DirectMessagePageModel(current, new User(user),
                new PrivateMessageLoader(db.PrivateMessages, current.Id, user.Id),
                new OldPrivateMessageLoader(db.OldPrivateMessages, current.Id, user.Id)));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Send(Guid id) 
        {
            string text;
            using (var s = new StreamReader(HttpContext.Request.Body))
            {
                text = await s.ReadToEndAsync();
            }
            var user = db.Users.FirstOrDefault(e => e.Id == id);
            if (user is null)
                return NotFound();
            
            var dt = DateTime.Now;
            var g = HttpContext.User.FindFirst(ClaimTypes.Name)!.Value;
            var sender = db.Users.Entry(Models.User.CurrentRecord[g]);
            var sendTo = db.Users.Entry(user);
            var rsa = RSA.Create();
            rsa.ImportRSAPublicKey(Models.User.CurrentRecord[g].PublicKey.AsSpan(), out int _);
            rsa.ImportEncryptedPkcs8PrivateKey(GlobalVals.ReadPassKey(sender.Entity.Id), sender.Entity.EncryptedPrivateKey, out var _);
            var bytes = Encoding.Unicode.GetBytes(text);
            var r = rsa.Encrypt(bytes, RSAEncryptionPadding.OaepSHA256);
            var rec = new PrivateMessageRecord
            {
                Message = r,
                FileCount = 0,
                SenderId = sender.Entity.Id,
                SentAt = dt,
                SentToId = sendTo.Entity.Id,
            };
            db.PrivateMessages.Add(rec);
            db.SaveChanges();
            return Ok(dt.ToShortTimeString());
        }
    }
}

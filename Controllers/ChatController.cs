using Beehive.Models;
using Beehive.Models.DbRecords;
using Beehive.Models.PageModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Beehive.Controllers
{
    public class ChatController(ApplicationContext ac, IHubContext<ChatHub> chatHub) : Controller
    {
        readonly ApplicationContext db = ac;
        private string CurrentId => HttpContext.User.FindFirst(ClaimTypes.Name)!.Value;
        private User Current => new(db.Users.First(e => e.Id == Guid.Parse(CurrentId)));
        private readonly IHubContext<ChatHub> hub = chatHub;

        [HttpGet]
        [Authorize]
        public IActionResult Search(string? query, bool includeDescriptions = false, bool global = false)
        {
            if (Models.User.Current is null) return Unauthorized();
            return View(new ChatSearchPageModel(db, Current, query, includeDescriptions, global));
        }

        [HttpGet]
        [Authorize]
        public IActionResult NewChat()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateChat(string title, string shortDesc, string longDesc)
        {
            var userId = Guid.Parse(CurrentId);
            var chat = new ChatRecord
            {
                Id = Guid.NewGuid(),
                Title = title,
                ShortDescription = shortDesc,
                LongDescription = longDesc,
                UserCount = 1
            };
            var memRec = new ChatMemberRecord
            {
                UserId = userId,
                ChatId = chat.Id
            };
            var adminRec = new ChatModRecord
            {
                UserId = userId,
                ChatId = chat.Id
            };
            db.Chats.Add(chat);
            db.ChatMemberRecs.Add(memRec);
            db.ChatModRecs.Add(adminRec);
            db.SaveChanges();
            hub.Groups.AddToGroupAsync(CurrentId, chat.Id.ToString());
            return View("Messages", new ChatMessagePageModel(Current, new Chat(chat), new ChatMessageLoader(db.ChatMessages, chat.Id),
                new OldChatMessageLoader(db.OldChatMessages, chat.Id)));
        }

        [Authorize]
        public IActionResult Join(Guid id)
        {
            var cr = db.Chats.FirstOrDefault(c => c.Id == id);
            if (cr is null) return NotFound();
            var userId = Guid.Parse(CurrentId);
            var memRec = new ChatMemberRecord
            {
                UserId = userId,
                ChatId = cr.Id
            };
            db.ChatMemberRecs.Add(memRec);
            db.SaveChanges();
            hub.Groups.AddToGroupAsync(CurrentId, cr.Id.ToString());
            return View("Messages", new ChatMessagePageModel(Current, new Chat(cr), new ChatMessageLoader(db.ChatMessages, cr.Id),
                new OldChatMessageLoader(db.OldChatMessages, cr.Id)));
        }

        [Authorize]
        public IActionResult Chat(Guid id)
        {
            var cr = db.Chats.FirstOrDefault(c => c.Id == id);
            if (cr is null) return NotFound();
            var chat = new Chat(cr);
            if (Current.GetChats(db).Contains(cr))
            {
                return View("Messages", new ChatMessagePageModel(Current, chat, new ChatMessageLoader(db.ChatMessages, chat.Id),
                    new OldChatMessageLoader(db.OldChatMessages, chat.Id)));
            }
            else
            {
                throw new NotImplementedException();
            }
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
            var chat = db.Chats.FirstOrDefault(c => c.Id == id);
            if (chat == null) return NotFound();

            var dt = DateTime.Now;
            var g = HttpContext.User.FindFirst(ClaimTypes.Name)!.Value;
            var sender = db.Users.Entry(Models.User.CurrentRecord[g]);
            var sendTo = db.Chats.Entry(chat);

            var rsa = RSA.Create();
            rsa.ImportRSAPublicKey(Models.User.CurrentRecord[g].PublicKey.AsSpan(), out int _);
            rsa.ImportEncryptedPkcs8PrivateKey(GlobalVals.ReadPassKey(sender.Entity.Id), sender.Entity.EncryptedPrivateKey, out var _);
            var bytes = Encoding.Unicode.GetBytes(text);
            var r = rsa.Encrypt(bytes, RSAEncryptionPadding.OaepSHA256);

            var rec = new ChatMessageRecord
            {
                Message = r,
                FileCount = 0,
                SenderId = sender.Entity.Id,
                SentAt = dt,
                ChatId = sendTo.Entity.Id,
            };
            db.ChatMessages.Add(rec);
            db.SaveChanges();
            return Ok(dt.ToShortTimeString());
        }
    }
}

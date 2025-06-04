using Beehive.Models.DbRecords;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Configuration;

namespace Beehive
{
    public class ChatHub(ApplicationContext ac) : Hub
    {
        readonly ApplicationContext db = ac;

        public async Task Send(string chatId, string text, string datetime)
        {
            await Clients.Caller.SendAsync("ReceiveSelf", text, datetime);
            await Clients.OthersInGroup(chatId).SendAsync("Receive", text, datetime);
        }

        public async void Add(string chatId, string userId)
        {
            await Groups.AddToGroupAsync(userId, chatId);
        }

        public async void Remove(string chatId, string userId)
        {
            await Groups.RemoveFromGroupAsync(userId, chatId);
        }

        public override Task OnConnectedAsync()
        {
            var user = db.Users.FirstOrDefault(e => e.Id.ToString() == Context.User!.Identity!.Name);
            if (user is not null)
            {
                var id = user.Id.ToString();
                foreach (var r in db.ChatMemberRecs.Where(e => e.Id == user.Id))
                    Groups.AddToGroupAsync(id, r.ChatId.ToString()).Start();
            }
            return base.OnConnectedAsync();
        }
    }
}

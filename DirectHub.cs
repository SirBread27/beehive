using Beehive.Models;
using Microsoft.AspNetCore.SignalR;

namespace Beehive
{
    public class DirectHub : Hub
    {
        public async Task Send(string id, string text, string datetime)
        {
            await Clients.Caller.SendAsync("ReceiveSelf", text, datetime);
            await Clients.User(id).SendAsync("Receive", text, datetime);
        }
    }
}

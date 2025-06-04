using Beehive.Models.DbRecords;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace Beehive.Models
{
    public class User(UserRecord record) : ModelBase
    {
        public static Dictionary<string, User> Current { get; } = [];
        
        public static Dictionary<string, UserRecord> CurrentRecord { get; } = [];

        public override Guid Id => record.Id;

        public string Name => record.Name;

        public string Email => record.Email;

        public string? Status => record.Status;

        public byte[] PublicKey => record.PublicKey;

        internal byte[] EncryptedPrivateKey => record.EncryptedPrivateKey;

        public List<ChatRecord> GetChats(ApplicationContext ac)
        {
            var t = ac.ChatMemberRecs.Where(e => e.UserId == Id).Select(e => e.ChatId);
            return [.. ac.Chats.Where(e => t.Contains(e.Id))];
        }

        //chats, pfp, banlist, posts
    }
}

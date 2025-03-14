using Beehive.Models.DbRecords;
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

        //chats, pfp, banlist, posts
    }
}

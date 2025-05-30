using Beehive.Models.DbRecords;
using Microsoft.EntityFrameworkCore;

namespace Beehive.Models.PageModels
{
    public class ChatSearchPageModel(ApplicationContext ac, User user, string? query = null, 
        bool includeDesc = false, bool global = false)
    {
        public string? Query { get; } = query;

        readonly DbSet<ChatRecord> set = ac.Chats;

        public readonly List<ChatRecord> UserChats = user.GetChats(ac);

        public IEnumerable<ChatRecord> ChatRes => Search().Take(250);

        public IEnumerable<ChatRecord> Search()
        {
            if (global) return GlobalSearch();
            if (string.IsNullOrEmpty(Query))
                return UserChats;
            else
                return set.Where(e => e.Title.Contains(Query) || (includeDesc && e.ShortDescription != null &&
                            e.ShortDescription.Contains(Query)) || (includeDesc && e.LongDescription != null &&
                            e.LongDescription.Contains(Query)))
                          .OrderByDescending(e => UserChats.Contains(e))
                          .ThenByDescending(e => e.Title.StartsWith(Query))
                          .ThenByDescending(e => e.Title.Contains(Query))
                          .ThenByDescending(e => e.UserCount)
                          .Take(250);
        }

        public IEnumerable<ChatRecord> GlobalSearch()
        {
            if (string.IsNullOrEmpty(Query))
                return set;
            else
                return set.Where(e => e.Title.Contains(Query))
                          .OrderByDescending(e => UserChats.Contains(e))
                          .ThenByDescending(e => e.Title.StartsWith(Query))
                          .ThenByDescending(e => e.Title.Contains(Query))
                          .ThenByDescending(e => e.UserCount)
                          .Take(250);
        }
    }
}

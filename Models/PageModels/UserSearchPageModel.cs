using Beehive.Models.DbRecords;
using Beehive.Models;
using Microsoft.EntityFrameworkCore;

namespace Beehive.Models.PageModels
{
    public class UserSearchPageModel(DbSet<UserRecord> _set, Guid userId, string? query = null)
    {
        public string? Query { get; } = query;

        readonly DbSet<UserRecord> set = _set;

        public IQueryable<UserRecord> Users => Search().Take(250);

        public IQueryable<UserRecord> Search()
        {
            if (string.IsNullOrEmpty(Query))
                return set.Where(e => e.Id != userId);
            else
                return set.Where(e => e.Id != userId)
                          .Where(e => e.Name.Contains(Query))
                          .OrderByDescending(e => e.Name.StartsWith(Query))
                          .Take(250);
        }
    }
}

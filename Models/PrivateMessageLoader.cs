using Beehive.Models.DbRecords;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Beehive.Models
{
    public class PrivateMessageLoader(DbSet<PrivateMessageRecord> set, Guid senderId, Guid userId) 
        : IEnumerable<Message>
    {
        public DbSet<PrivateMessageRecord> Set => set;

        public Guid SenderId => senderId;

        public Guid UserId => userId;

        public IEnumerator<Message> GetEnumerator()
        {
            return Set.Where(e => (e.SentTo.Id == UserId && e.SentBy.Id == SenderId) 
                        || (e.SentTo.Id == SenderId && e.SentBy.Id == UserId))
                      .OrderBy(e => e.SentAt)
                      .Include(e => e.SentTo)
                      .Include(e => e.SentBy)
                      .Select(e => new Message(e))
                      .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

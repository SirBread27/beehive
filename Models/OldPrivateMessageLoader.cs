using Beehive.Models.DbRecords;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Beehive.Models
{
    public class OldPrivateMessageLoader(DbSet<OldPrivateMessageRecord> set, Guid senderId, Guid userId)
        : IEnumerable<Message>
    {
        public DbSet<OldPrivateMessageRecord> Set => set;

        public Guid SenderId => senderId;

        public Guid UserId => userId;

        public IEnumerator<Message> GetEnumerator()
        {
            return Set.OrderBy(e => e.SentAt)
                      .Where(e => (e.SentTo.Id == UserId && e.SentBy.Id == SenderId)
                        || (e.SentToId == SenderId && e.SenderId == UserId))
                      .Include(e => e.SentTo)
                      .Include(e => e.SentBy)
                      .Select(e => new Message(e))
                      .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

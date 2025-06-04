using Beehive.Models.DbRecords;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Beehive.Models
{
    public class ChatMessageLoader(DbSet<ChatMessageRecord> set, Guid chatId)
        : IEnumerable<Message>
    {
        public DbSet<ChatMessageRecord> Set => set;

        public Guid ChatId => chatId;

        public IEnumerator<Message> GetEnumerator()
        {
            return Set.Where(e => e.Chat.Id == ChatId)
                      .OrderBy(e => e.SentAt)
                      .Include(e => e.Chat)
                      .Include(e => e.SentBy)
                      .Select(e => new Message(e))
                      .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

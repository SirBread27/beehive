using Beehive.Models.DbRecords;

namespace Beehive.Models
{
    public class Message : ModelBase
    {
        public override Guid Id { get; } = Guid.NewGuid();

        public byte[] Text { get; set; } = null!;

        public DateTime SentAt { get; set; }

        public User Sender { get; set; } = null!;

        public byte FileCount { get; set; }

        //files, re-sent, answer

        public Message() { }

        public Message(MessageRecord record)
        {
            Text = record.Message;
            SentAt = record.SentAt;
            Sender = new User(record.SentBy);
            FileCount = record.FileCount;
        }
    }
}

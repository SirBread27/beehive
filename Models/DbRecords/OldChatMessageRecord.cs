using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beehive.Models.DbRecords
{
    [Table("OldChatMessages")]
    public class OldChatMessageRecord : MessageRecord
    {
        [Required]
        [ForeignKey("Chat")]
        public Guid ChatId { get; set; }

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public ChatRecord Chat { get; set; } = null!;

        public static implicit operator ChatMessageRecord(OldChatMessageRecord c) => new() { Chat = c.Chat };
        public static implicit operator OldChatMessageRecord(ChatMessageRecord c) => new() { Chat = c.Chat };
    }
}

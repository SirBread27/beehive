using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beehive.Models.DbRecords
{
    [Table("ChatMessages")]
    public class ChatMessageRecord : MessageRecord
    {
        [Required]
        [ForeignKey("Chat")]
        public Guid ChatId { get; set; }

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public ChatRecord Chat { get; set; } = null!;
    }
}

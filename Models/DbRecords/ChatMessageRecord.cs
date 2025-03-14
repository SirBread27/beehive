using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beehive.Models.DbRecords
{
    [Table("ChatMessages")]
    public class ChatMessageRecord : MessageRecord
    {
        [Required]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        [ForeignKey("chatID")]
        public ChatRecord Chat { get; set; } = null!;
    }
}

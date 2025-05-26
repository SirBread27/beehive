using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beehive.Models.DbRecords
{
    [Table("ChatMemberRecs")]
    [PrimaryKey("Id")]
    public class ChatMemberRecord
    {
        [Required]
        [Column("recID")]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public UserRecord User { get; set; } = null!;

        [Required]
        [ForeignKey("Chat")]
        public Guid ChatId { get; set; }

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public ChatRecord Chat { get; set; } = null!;
    }
}

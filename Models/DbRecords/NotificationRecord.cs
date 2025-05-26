using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beehive.Models.DbRecords
{
    [Table("Notifications")]
    [PrimaryKey("Id")]
    public class NotificationRecord
    {
        [Required]
        [Column("notifID")]
        public Guid Id { get; set; }

        [Required]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        [ForeignKey("userID")]
        public UserRecord User { get; set; } = null!;

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Text { get; set; } = string.Empty;

        [Required]
        public bool IsRead { get; set; }

        [Required]
        public DateTime SentAt { get; set; }

        public string? Url { get; set; }

        [DeleteBehavior(DeleteBehavior.NoAction)]
        [ForeignKey("imgID")]
        public FileRecord? Image { get; set; }
    }
}

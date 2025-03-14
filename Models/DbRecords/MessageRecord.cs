using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beehive.Models.DbRecords
{
    [Table("Messages")]
    [PrimaryKey("Id")]
    public class MessageRecord
    {
        [Required]
        [Column("msgID")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Message { get; set; } = string.Empty;

        [Required]
        public byte FileCount { get; set; }

        [Required]
        public Guid SenderId { get; set; }

        [ForeignKey("SenderId")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public UserRecord SentBy { get; set; } = null!;

        [Required]
        public DateTime SentAt { get; set; }

        public Guid? ResentMessage { get; set; }

        public Guid? AnswerTo { get; set; }
    }
}

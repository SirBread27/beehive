using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beehive.Models.DbRecords
{
    [Table("OldPrivateMessages")]
    public class OldPrivateMessageRecord : MessageRecord
    {
        [Required]
        [ForeignKey("SentTo")]
        public Guid SentToId { get; set; }

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public UserRecord SentTo { get; set; } = null!;

        public static implicit operator PrivateMessageRecord(OldPrivateMessageRecord c) => new() { SentTo = c.SentTo };
        public static implicit operator OldPrivateMessageRecord(PrivateMessageRecord c) => new() { SentTo = c.SentTo };
    }
}

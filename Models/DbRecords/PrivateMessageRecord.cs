using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beehive.Models.DbRecords
{
    [Table("PrivateMessages")]
    public class PrivateMessageRecord : MessageRecord
    {
        [Required]
        [ForeignKey("SentTo")]
        public Guid SentToId { get; set; }

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public UserRecord SentTo { get; set; } = null!;
    }
}

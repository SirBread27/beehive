using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beehive.Models.DbRecords
{
    [Table("PrivateMessages")]
    public class PrivateMessageRecord : MessageRecord
    {
        [Required]
        public Guid SentToId { get; set; }

        [ForeignKey("SentToId")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public UserRecord SentTo { get; set; } = null!;
    }
}

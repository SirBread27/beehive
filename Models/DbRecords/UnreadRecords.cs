using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beehive.Models.DbRecords
{
    [Table("UnreadRecs")]
    [PrimaryKey("Id")]
    public class UnreadRecords
    {
        [Required]
        [Column("recID")]
        public Guid Id { get; set; }

        [Required]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        [ForeignKey("userID")]
        public UserRecord User { get; set; } = null!;

        [Required]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        [ForeignKey("msgID")]
        public MessageRecord Message { get; set; } = null!;
    }
}

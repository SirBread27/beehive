using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beehive.Models.DbRecords
{
    [Table("ChatNeighborRecs")]
    [PrimaryKey("Id")]
    public class ChatNeighboringRecord
    {
        [Required]
        [Column("recID")]
        public Guid Id { get; set; }

        [Required]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        [ForeignKey("chat1ID")]
        public ChatRecord Chat1 { get; set; } = null!;

        [Required]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        [ForeignKey("chat2ID")]
        public ChatRecord Chat2 { get; set; } = null!;
    }
}

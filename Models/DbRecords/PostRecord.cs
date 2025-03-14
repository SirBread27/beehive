using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beehive.Models.DbRecords
{
    [Table("Posts")]
    [PrimaryKey("Id")]
    public class PostRecord
    {
        [Required]
        [Column("postID")]
        public Guid Id { get; set; }

        [Required]
        public string Text { get; set; } = "";

        [Required]
        public DateTime PostedAt { get; set; }

        [Required]
        public int FileCount { get; set; }

        [Required]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        [ForeignKey("userID")]
        public UserRecord User { get; set; } = null!;
    }
}

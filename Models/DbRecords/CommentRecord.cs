using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beehive.Models.DbRecords
{
    [Table("Comments")]
    [PrimaryKey("Id")]
    public class CommentRecord
    {
        [Required]
        [Column("commentID")]
        public Guid Id { get; set; }

        [Required]
        public string Text { get; set; } = "";

        [Required]
        public DateTime PostedAt { get; set; }

        [Required]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        [ForeignKey("userID")]
        public UserRecord User { get; set; } = null!;

        [Required]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        [ForeignKey("postID")]
        public PostRecord Post { get; set; } = null!;
    }
}

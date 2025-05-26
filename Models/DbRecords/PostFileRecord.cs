using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beehive.Models.DbRecords
{
    [Table("PostFileRecs")]
    [PrimaryKey("Id")]
    public class PostFileRecord
    {
        [Required]
        [Column("recID")]
        public Guid Id { get; set; }

        [Required]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        [ForeignKey("postID")]
        public PostRecord Post { get; set; } = null!;

        [Required]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        [ForeignKey("fileID")]
        public FileRecord File { get; set; } = null!;
    }
}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beehive.Models.DbRecords
{
    [PrimaryKey("Id")]
    [Table("Cells")]
    public class CellRecord
    {
        [Required]
        [Column("cellID")]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        public string? ShortDescription { get; set; }

        public string? LongDescription { get; set; }

        [DeleteBehavior(DeleteBehavior.NoAction)]
        [ForeignKey("pfpID")]
        public FileRecord? Pfp { get; set; }
    }
}

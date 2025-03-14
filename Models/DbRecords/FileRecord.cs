using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beehive.Models.DbRecords
{
    [Table("Files")]
    [PrimaryKey("Id")]
    public class FileRecord
    {
        [Required]
        [Column("fileID")]
        public Guid Id { get; set; }

        [Required]
        public string Location { get; set; } = null!;

        [Required]
        public int UseCount { get; set; }
    }
}

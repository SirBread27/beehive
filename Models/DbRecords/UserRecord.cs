using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beehive.Models.DbRecords
{
    [PrimaryKey("Id")]
    [Table("Users")]
    public class UserRecord
    {
        [Column("userID")]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [EmailAddress]
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public int Password { get; set; }

        public string? Status { get; set; }

        [DeleteBehavior(DeleteBehavior.NoAction)]
        [ForeignKey("pfpID")]
        public FileRecord? Pfp { get; set; }
    }
}

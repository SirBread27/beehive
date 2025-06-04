using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Beehive.Models.DbRecords
{
    [Table("Chats")]
    [PrimaryKey("Id")]
    public class ChatRecord
    {
        internal object AvatarUrl;

        [Required]
        [Column("chatID")]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        public string? ShortDescription { get; set; }

        public string? LongDescription { get; set; }

        [Required]
        public int UserCount { get; set; } = 0;

        [AllowNull]
        [ForeignKey("Pfp")]
        public Guid? PfpId { get; set; } = null;

        public FileRecord? Pfp { get; set; }

        [AllowNull]
        [ForeignKey("Cell")]
        public Guid? CellId { get; set; } = null;

        public CellRecord Cell { get; set; } = null!;

        public override bool Equals(object? obj)
        {
            if (obj is null) return this is null;
            if (obj is not ChatRecord cr) return false;
            return cr.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}

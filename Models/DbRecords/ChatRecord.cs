using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beehive.Models.DbRecords
{
    [Table("Chats")]
    [PrimaryKey("Id")]
    public class ChatRecord
    {
        [Required]
        [Column("chatID")]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        public string? ShortDescription { get; set; }

        public string? LongDescription { get; set; }

        [ForeignKey("pfpID")]
        public FileRecord? Pfp { get; set; }

        [Required]
        [ForeignKey("cellID")]
        public CellRecord Cell { get; set; } = null!;
    }
}

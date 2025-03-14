using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beehive.Models.DbRecords
{
    [PrimaryKey("Id")]
    [Table("UserBanRecs")]
    public class BanListRecord
    {
        [Column("recID")]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        [ForeignKey("userID")]
        public UserRecord User { get; set; } = null!;

        [Required]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        [ForeignKey("bannedUserID")]
        public UserRecord BannedUser { get; set; } = null!;
    }
}

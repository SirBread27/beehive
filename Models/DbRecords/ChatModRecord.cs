﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beehive.Models.DbRecords
{
    [Table("ChatModRecs")]
    [PrimaryKey("Id")]
    public class ChatModRecord
    {
        [Required]
        [Column("recID")]
        public Guid Id { get; set; }

        [Required]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        [ForeignKey("userID")]
        public UserRecord User { get; set; } = null!;

        [Required]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        [ForeignKey("chatID")]
        public ChatRecord Chat { get; set; } = null!;

        [Required]
        public byte ModLevel { get; set; }
    }
}

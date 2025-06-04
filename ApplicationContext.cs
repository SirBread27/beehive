using Beehive.Models.DbRecords;
using Microsoft.EntityFrameworkCore;

namespace Beehive
{
    public class ApplicationContext : DbContext
    {
        public DbSet<BanListRecord> UserBanRecs { get; set; } = null!;
        public DbSet<CellRecord> Cells { get; set; } = null!;
        public DbSet<ChatBanRecord> ChatBanRecs { get; set; } = null!;
        public DbSet<ChatMemberRecord> ChatMemberRecs { get; set; } = null!;
        public DbSet<ChatMessageRecord> ChatMessages { get; set; } = null!;
        public DbSet<ChatNeighboringRecord> ChatNeighboringRecs { get; set; } = null!;
        public DbSet<ChatRecord> Chats { get; set; } = null!;
        public DbSet<CommentRecord> Comments { get; set; } = null!;
        public DbSet<FileRecord> Files { get; set; } = null!;
        public DbSet<MessageFileRecord> MsgFiles { get; set; } = null!;
        public DbSet<MessageRecord> Messages { get; set; } = null!;
        public DbSet<NotificationRecord> Notifications { get; set; } = null!;
        public DbSet<OldChatMessageRecord> OldChatMessages { get; set; } = null!;
        public DbSet<OldPrivateMessageRecord> OldPrivateMessages { get; set; } = null!;
        public DbSet<PostFileRecord> PostFiles { get; set; } = null!;
        public DbSet<PostRecord> Posts { get; set; } = null!;
        public DbSet<PrivateMessageRecord> PrivateMessages { get; set; } = null!;
        public DbSet<UnreadRecords> UnreadRecs { get; set; } = null!;
        public DbSet<UserRecord> Users { get; set; } = null!;
        public DbSet<ChatModRecord> ChatModRecs { get; set; } = null!;

        // Data/ApplicationContext.cs

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureDeleted(); //DEBUG
            Database.EnsureCreated();   
        }

        public ApplicationContext(string connection) : base()
        {
            var b = new DbContextOptionsBuilder<ApplicationContext>().UseSqlServer(connection);
            OnConfiguring(b);
        }

        public ApplicationContext() : base() { }
    }
}

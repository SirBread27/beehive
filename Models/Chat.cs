using Beehive.Models.DbRecords;

namespace Beehive.Models
{
    public class Chat(ChatRecord record) : ModelBase
    {
        public override Guid Id => record.Id;

        public string Title => record.Title;

        public string? ShortDescription => record.ShortDescription;

        public string? LongDescription => record.LongDescription;

        public int UserCount { get; set; } = 0;

        public string? AvatarUrl { get; set; }

        //TODO pfp, cells
    }
}

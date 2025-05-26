namespace Beehive.Models.PageModels
{
    public class DirectMessagePageModel(User me, User companion, PrivateMessageLoader loader,
        OldPrivateMessageLoader archiveLoader)
    {
        public User Current => me;

        public User Companion => companion;

        public string MessageText { get; set; } = string.Empty;

        public PrivateMessageLoader Loader => loader;

        public OldPrivateMessageLoader ArchiveLoader => archiveLoader;

        public List<Message> Messages => [.. EnumerationModel.Take(250)];

        public readonly IEnumerable<Message> EnumerationModel = loader.Concat(archiveLoader);
    }
}

namespace Beehive.Models.PageModels
{
    public class DirectMessagePageModel(User me, User companion, PrivateMessageLoader loader,
        OldPrivateMessageLoader archiveLoader)
    {
        public User Current => me;

        public User Companion => companion;

        public string MessageText { get; set; } = "";

        public PrivateMessageLoader Loader => loader;

        public OldPrivateMessageLoader ArchiveLoader => archiveLoader;

        public List<Message> Messages => [.. loader.Concat(archiveLoader).Take(250)];  
    }
}

using Beehive.Models.PageModels;
using System.Collections.Generic;

namespace Beehive.Web.Models.Messenger
{
    public class MessengerPageModel
    {
        // Список диалогов
        public List<ConversationModel> Conversations { get; set; } = new();

        // Активный выбранный диалог (если выбран)
        public ConversationModel? ActiveConversation { get; set; }

        // Текущий пользователь (тот, кто авторизован)
        public UserModel CurrentUser { get; set; } = null!;
    }
}
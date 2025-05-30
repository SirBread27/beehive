using Beehive.Models.PageModels;
using System;

namespace Beehive.Web.Models.Messenger
{
    public class ConversationModel
    {
        public UserModel User { get; set; } = null!; // Пользователь с кем переписка

        public List<MessageModel> Messages { get; set; } = new();

        // Последнее сообщение для краткого отображения
        public MessageModel? LastMessage => Messages.Count > 0 ? Messages[^1] : null;
    }
}
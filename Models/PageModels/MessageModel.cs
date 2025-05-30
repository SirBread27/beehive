using System;

namespace Beehive.Web.Models.Messenger
{
    public class MessageModel
    {
        public string Text { get; set; } = null!; // Текст сообщения

        public DateTime SentAt { get; set; } // Время отправки сообщения

        public bool IsCurrentUser { get; set; } // Флаг, указывающий, отправлено ли сообщение текущим пользователем
    }
}
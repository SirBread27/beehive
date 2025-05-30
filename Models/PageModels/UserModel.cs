namespace Beehive.Models.PageModels
{
    public class UserModel
    {
        public string Id { get; set; } = null!; // Уникальный идентификатор пользователя (например GUID или строка)
        public string Name { get; set; } = null!;
        public string Status { get; set; } = ""; // Статус или описание (например "Онлайн", "Не в сети")
    }
}

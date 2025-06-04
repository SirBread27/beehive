using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

public class FriendsController : Controller
{
    public IActionResult Index()
    {
        // Создаем и заполняем список друзей
        var friends = new List<Friend>
        {
            new Friend { Id = 1, Name = "Иван Пчелкин", Avatar = "/images/avatar1.jpg", IsOnline = true, LastSeen = null },
            new Friend { Id = 2, Name = "Мария Пчелова", Avatar = "/images/avatar2.jpg", IsOnline = false, LastSeen = "2 часа назад" },
            new Friend { Id = 3, Name = "Алексей Ульев", Avatar = "/images/avatar3.jpg", IsOnline = true, LastSeen = null }
        };

        // Явно передаем модель в представление
        return View("~/Views/Home/Friends.cshtml", friends);
    }
}

public class Friend
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Avatar { get; set; }
    public bool IsOnline { get; set; }
    public string LastSeen { get; set; }
}
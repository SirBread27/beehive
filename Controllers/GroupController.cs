using Microsoft.AspNetCore.Mvc;

public class GroupController : Controller
{
    // GET: /GroupChat/Index
    public IActionResult chat1()
    {
        return View();
    }

    // Дополнительные методы для работы с групповым чатом
    // Например, для получения/отправки сообщений и т.д.

    [HttpPost]
    public IActionResult SendMessage(string message)
    {
        // Здесь будет логика сохранения сообщения в базу данных
        // и отправка его другим участникам чата

        // Пока просто возвращаем успешный статус
        return Json(new { success = true });
    }

    [HttpGet]
    public IActionResult GetMessages()
    {
        // Здесь будет логика получения сообщений из базы данных
        // Пока возвращаем тестовые данные
        var messages = new[]
        {
            new { sender = "Мария Пчелкина", time = "10:30", text = "Привет всем! Кто будет на выставке меда в эти выходные?" },
            new { sender = "Вы", time = "10:32", text = "Я планирую быть там в субботу утром" },
            new { sender = "Алексей Пчелов", time = "12:45", text = "Я тоже буду, можем встретиться у входа в 11:00" },
            new { sender = "Дмитрий Ульев", time = "12:47", text = "Я привезу образцы нового меда, можете попробовать" }
        };

        return Json(messages);
    }
}
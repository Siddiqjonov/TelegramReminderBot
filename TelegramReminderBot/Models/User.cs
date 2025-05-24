namespace TelegramReminderBot.Models;

public class User
{
    public int Id { get; set; }
    public long TelegramId { get; set; }
    public string FullName { get; set; }
}

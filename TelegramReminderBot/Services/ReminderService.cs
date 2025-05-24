using TelegramReminderBot.Data;
using TelegramReminderBot.Models;

namespace TelegramReminderBot.Services;

public class ReminderService
{
    private readonly AppDbContext _db;
    private readonly TelegramService _tg;

    public ReminderService(AppDbContext db, TelegramService tg)
    {
        _db = db;
        _tg = tg;
    }

    public async Task SendRemindersAsync()
    {
        var users = _db.Users.ToList();
        var message = "🗑️ Please take out the garbage!";

        foreach (var user in users)
        {
            await _tg.SendMessageAsync(user.TelegramId, message);
        }

        // Send to group (replace with your group chat id)
        long groupId = -1001234567890;
        await _tg.SendMessageAsync(groupId, message);
    }
}

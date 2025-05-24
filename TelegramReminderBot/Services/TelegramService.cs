using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace TelegramReminderBot.Services;

public class TelegramService
{
    private readonly string _botToken;
    private readonly HttpClient _http;

    public TelegramService(IConfiguration config)
    {
        _botToken = config["TelegramBotToken"];
        _http = new HttpClient();
    }

    public async Task SendMessageAsync(long chatId, string text)
    {
        var url = $"https://api.telegram.org/bot{_botToken}/sendMessage";
        var payload = new { chat_id = chatId, text = text };

        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        await _http.PostAsync(url, content);
    }
}

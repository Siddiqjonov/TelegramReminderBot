using Microsoft.AspNetCore.Mvc;

namespace TelegramReminderBot.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TelegramController : ControllerBase
{
    [HttpPost]
    public IActionResult ReceiveUpdate([FromBody] object update)
    {
        // Just receive and ignore Telegram updates (optional)
        return Ok();
    }
}

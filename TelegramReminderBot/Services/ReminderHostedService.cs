namespace TelegramReminderBot.Services;

public class ReminderHostedService : BackgroundService
{
    private readonly IServiceProvider _provider;
    private readonly TimeSpan _scheduledTime = new TimeSpan(6, 0, 0); // 6 AM

    public ReminderHostedService(IServiceProvider provider)
    {
        _provider = provider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now;
            var nextRunTime = DateTime.Today.Add(_scheduledTime);
            if (now > nextRunTime)
                nextRunTime = nextRunTime.AddDays(1);

            var delay = nextRunTime - now;
            await Task.Delay(delay, stoppingToken);

            using (var scope = _provider.CreateScope())
            {
                var reminderService = scope.ServiceProvider.GetRequiredService<ReminderService>();
                await reminderService.SendRemindersAsync();
            }

            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }
}

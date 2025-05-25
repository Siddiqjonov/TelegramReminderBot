
using Microsoft.EntityFrameworkCore;
using TelegramReminderBot.Data;
using TelegramReminderBot.Services;

namespace TelegramReminderBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Set port for deployment (Railway) but allow local default behavior
            var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
            if (!builder.Environment.IsDevelopment())
            {
                builder.WebHost.UseUrls($"http://*:{port}");
            }
            builder.Services.AddHealthChecks();


            builder.Services.AddControllers();
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<TelegramService>();
            builder.Services.AddScoped<ReminderService>();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddHostedService<ReminderHostedService>();


            var app = builder.Build();

            app.UseHealthChecks("/health");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

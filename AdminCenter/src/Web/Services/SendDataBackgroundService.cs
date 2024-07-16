
using AdminCenter.Domain;
using AdminCenter.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace AdminCenter.Web.Services;

public class SendDataBackgroundService(IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await db.Database.MigrateAsync();

        if (!await db.Users.AnyAsync())
        {
            db.Users.Add(new User(Guid.NewGuid(), "admin", "admin", "张三"));
            await db.SaveChangesAsync();
        }
    }
}

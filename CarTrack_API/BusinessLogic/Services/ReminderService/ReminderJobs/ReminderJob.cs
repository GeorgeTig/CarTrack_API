using CarTrack_API.BusinessLogic.Services.ReminderService.ReminderHubs;
using Microsoft.AspNetCore.SignalR;
using Quartz;

namespace CarTrack_API.BusinessLogic.Services.ReminderService.ReminderJobs;

public class ReminderJob(IServiceScopeFactory scopeFactory) : IJob
{
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;

    public async Task Execute(IJobExecutionContext context)
    {
        using var scope = _scopeFactory.CreateScope();
        
        var reminderService = scope.ServiceProvider.GetRequiredService<IReminderService>();
        var hubContext = scope.ServiceProvider.GetRequiredService<IHubContext<ReminderHub>>();

        double daysPassed = 1.0; 
        if (context.PreviousFireTimeUtc.HasValue)
        {
            var timeSinceLastFire = DateTime.UtcNow - context.PreviousFireTimeUtc.Value;
            daysPassed = timeSinceLastFire.TotalDays;
        }

        await reminderService.ProcessReminderUpdatesAsync(daysPassed);
        
        await hubContext.Clients.All.SendAsync("UpdateReminders");
    }
}
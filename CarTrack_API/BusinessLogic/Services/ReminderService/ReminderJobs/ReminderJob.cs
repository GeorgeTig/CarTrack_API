using CarTrack_API.BusinessLogic.Services.ReminderService.ReminderHubs;
using Microsoft.AspNetCore.SignalR;
using Quartz;

namespace CarTrack_API.BusinessLogic.Services.ReminderService.ReminderJobs;

public class ReminderJob(IServiceScopeFactory scopeFactory, IReminderService reminderService) : IJob
{
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
    private readonly IReminderService _reminderService = reminderService;

    public async Task Execute(IJobExecutionContext context)
    {
        using var scope = _scopeFactory.CreateScope();
        var hubContext = scope.ServiceProvider.GetRequiredService<IHubContext<ReminderHub>>();

        await _reminderService.ActualizeRemindersDueAsync();
        
        await hubContext.Clients.All.SendAsync("UpdateReminders");
    }
}
using Microsoft.AspNetCore.SignalR;

namespace CarTrack_API.BusinessLogic.Services.ReminderService.ReminderHubs;

public class ReminderHub : Hub
{
    public async Task SendReminder()
    {
        await Clients.All.SendAsync("UpdateReminders");
    }
}
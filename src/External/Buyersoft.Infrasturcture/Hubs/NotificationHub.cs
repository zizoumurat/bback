using Microsoft.AspNetCore.SignalR;

namespace Buyersoft.Infrastructure.Hubs;
public class NotificationHub : Hub
{
    public async Task JoinGroup(string requestId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, requestId);
    }

    public async Task LeaveGroup(string requestId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, requestId);
    }

    public async Task UpdatePrice(string requestId, decimal newPrice)
    {
        await Clients.Group(requestId).SendAsync("ReceivePriceUpdate", newPrice);
    }

    public async Task StartCountdown(string requestId)
    {
        await Clients.Group(requestId).SendAsync("StartCountdown");
    }

    public async Task SendComment(string contractId, string user, string message)
    {
        // Tüm istemcilere mesajı yayınla (belirli bir sözleşme için)
        await Clients.Group(contractId).SendAsync("ReceiveComment", user, message);
    }

    public async Task JoinContractGroup(string contractId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, contractId);
    }

    public async Task LeaveContractGroup(string contractId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, contractId);
    }

    public async Task JoinUserGroup(string userId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, userId);
    }

    public async Task LeaveUserGroup(string contractId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, contractId);
    }

}

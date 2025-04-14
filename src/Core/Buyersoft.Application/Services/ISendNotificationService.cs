using Buyersoft.Domain.Enums;

namespace Buyersoft.Application.Services;

public interface ISendNotificationService
{
    Task SendNotificationToUserAsync(string userId, string message);
    Task ChangeStatu(string requestId, ReverseAuctionStatusEnum statu, int remainingSeconds);
    Task ChangePrice(string requestId);

    Task SendComment(string contractId, string user, string message);
}
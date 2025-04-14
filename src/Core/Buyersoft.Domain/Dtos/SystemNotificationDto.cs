using Buyersoft.Domain.Enums;

namespace Buyersoft.Domain.Dtos;

public sealed record SystemNotificationDto(int Id, string Code, string Message, SystemNotificationEnum Type);

public sealed record SystemNotificationFilterDto(SystemNotificationEnum? Type, string Message, string Code);

public class SystemNotificationListDto()
{
    public int Id { get; set; }
    public SystemNotificationEnum Type { get; set; }
    public string Message { get; set; }
    public string Code { get; set; }
}
using Buyersoft.Domain.Enums;

namespace Buyersoft.Domain.Entitites;

public class SystemNotification : BaseEntity
{
    public string Code { get; set; }
    public string Message { get; set; }
    public SystemNotificationEnum Type { get; set; }
}
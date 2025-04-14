using Buyersoft.Domain.Entitites.Identity;
using Buyersoft.Domain.Enums;

namespace Buyersoft.Domain.Entitites;

public class UserNotificationPreference : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; }

    public NotificationMethod Method { get; set; }
}

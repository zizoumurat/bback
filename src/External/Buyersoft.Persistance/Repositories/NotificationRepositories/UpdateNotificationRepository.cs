using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.NotificationRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.NotificationRepositories;

public class UpdateNotificationRepository : UpdateRepository<Notification>, IUpdateNotificationRepository
{
    public UpdateNotificationRepository(BaseDbContext context) : base(context)
    {
    }
}


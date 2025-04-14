using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.SystemNotificationRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.SystemNotificationRepositories;

public class UpdateSystemNotificationRepository : UpdateRepository<SystemNotification>, IUpdateSystemNotificationRepository
{
    public UpdateSystemNotificationRepository(BaseDbContext context) : base(context)
    {
    }
}


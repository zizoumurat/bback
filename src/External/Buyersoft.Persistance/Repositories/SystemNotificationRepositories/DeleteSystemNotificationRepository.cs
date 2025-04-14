using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.SystemNotificationRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.SystemNotificationRepositories;

public class DeleteSystemNotificationRepository : DeleteRepository<SystemNotification>, IDeleteSystemNotificationRepository
{
    public DeleteSystemNotificationRepository(BaseDbContext context) : base(context)
    {
    }
}

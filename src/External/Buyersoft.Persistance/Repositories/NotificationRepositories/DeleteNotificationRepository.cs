using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.NotificationRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.NotificationRepositories;

public class DeleteNotificationRepository : DeleteRepository<Notification>, IDeleteNotificationRepository
{
    public DeleteNotificationRepository(BaseDbContext context) : base(context)
    {
    }
}

using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.OrderRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.OrderRepositories;

public class DeleteOrderRepository : DeleteRepository<Order>, IDeleteOrderRepository
{
    public DeleteOrderRepository(BaseDbContext context) : base(context)
    {
    }
}

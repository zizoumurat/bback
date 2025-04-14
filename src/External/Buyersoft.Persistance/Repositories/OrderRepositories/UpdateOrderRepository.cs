using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.OrderRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.OrderRepositories;

public class UpdateOrderRepository : UpdateRepository<Order>, IUpdateOrderRepository
{
    public UpdateOrderRepository(BaseDbContext context) : base(context)
    {
    }
}


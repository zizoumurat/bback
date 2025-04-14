using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.OrderRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.OrderRepositories;

public class QueryOrderRepository : QueryRepository<Order>, IQueryOrderRepository
{
    public QueryOrderRepository(BaseDbContext context) : base(context)
    {
    }
}

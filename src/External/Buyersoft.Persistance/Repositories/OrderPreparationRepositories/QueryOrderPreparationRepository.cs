using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.OrderPreparationRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.OrderPreparationRepositories;

public class QueryOrderPreparationRepository : QueryRepository<OrderPreparation>, IQueryOrderPreparationRepository
{
    public QueryOrderPreparationRepository(BaseDbContext context) : base(context)
    {
    }
}

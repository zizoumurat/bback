using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.ReturnItemRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.ReturnItemRepositories;

public class QueryReturnItemRepository : QueryRepository<ReturnItem>, IQueryReturnItemRepository
{
    public QueryReturnItemRepository(BaseDbContext context) : base(context)
    {
    }
}

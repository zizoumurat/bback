using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.RequestGroupRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.RequestGroupRepositories;

public class QueryRequestGroupRepository : QueryRepository<RequestGroup>, IQueryRequestGroupRepository
{
    public QueryRequestGroupRepository(BaseDbContext context) : base(context)
    {
    }
}

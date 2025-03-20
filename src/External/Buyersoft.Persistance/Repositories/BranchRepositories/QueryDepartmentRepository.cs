using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.BranchRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.BranchRepositories;

public class QueryBranchRepository : QueryRepository<Branch>, IQueryBranchRepository
{
    public QueryBranchRepository(BaseDbContext context) : base(context)
    {
    }
}

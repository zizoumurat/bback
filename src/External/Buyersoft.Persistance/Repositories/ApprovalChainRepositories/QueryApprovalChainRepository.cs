using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.ApprovalChainRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.ApprovalChainRepositories;

public class QueryApprovalChainRepository : QueryRepository<ApprovalChain>, IQueryApprovalChainRepository
{
    public QueryApprovalChainRepository(BaseDbContext context) : base(context)
    {
    }
}

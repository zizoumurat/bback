using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.ApprovalChainUserRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.ApprovalChainUserRepositories;

public class QueryApprovalChainUserRepository : QueryRepository<ApprovalChainUser>, IQueryApprovalChainUserRepository
{
    public QueryApprovalChainUserRepository(BaseDbContext context) : base(context)
    {
    }
}

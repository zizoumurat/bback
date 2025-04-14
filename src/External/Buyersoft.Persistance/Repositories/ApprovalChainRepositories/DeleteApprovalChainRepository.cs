using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.ApprovalChainRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.ApprovalChainRepositories;

public class DeleteApprovalChainRepository : DeleteRepository<ApprovalChain>, IDeleteApprovalChainRepository
{
    public DeleteApprovalChainRepository(BaseDbContext context) : base(context)
    {
    }
}

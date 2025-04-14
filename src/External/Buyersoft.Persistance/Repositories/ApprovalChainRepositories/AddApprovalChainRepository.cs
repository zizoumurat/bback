using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.ApprovalChainRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.AddApprovalChainRepositories;
public class AddApprovalChainRepository : AddRepository<ApprovalChain>, IAddApprovalChainRepository
{
    public AddApprovalChainRepository(BaseDbContext context) : base(context)
    {
    }
}

using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.ApprovalChainUserRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.AddApprovalChainUserRepositories;
public class AddApprovalChainUserRepository : AddRepository<ApprovalChainUser>, IAddApprovalChainUserRepository
{
    public AddApprovalChainUserRepository(BaseDbContext context) : base(context)
    {
    }
}

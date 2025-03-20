using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.ApprovalChainUserRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.ApprovalChainUserRepositories;

public class UpdateApprovalChainUserRepository : UpdateRepository<ApprovalChainUser>, IUpdateApprovalChainUserRepository
{
    public UpdateApprovalChainUserRepository(BaseDbContext context) : base(context)
    {
    }
}


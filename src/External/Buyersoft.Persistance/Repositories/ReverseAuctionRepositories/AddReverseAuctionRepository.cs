using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.ReverseAuctionRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.RolePermissionRepositories;
public class AddReverseAuctionRepository : AddRepository<ReverseAuction>, IAddReverseAuctionRepository
{
    public AddReverseAuctionRepository(BaseDbContext context) : base(context)
    {
    }
}

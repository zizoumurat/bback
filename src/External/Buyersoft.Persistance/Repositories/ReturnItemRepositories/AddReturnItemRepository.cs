using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.ReturnItemRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.ReturnItemRepositories;
public class AddReturnItemRepository : AddRepository<ReturnItem>, IAddReturnItemRepository
{
    public AddReturnItemRepository(BaseDbContext context) : base(context)
    {
    }
}

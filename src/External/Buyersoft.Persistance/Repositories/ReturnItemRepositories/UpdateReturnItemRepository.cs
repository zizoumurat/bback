using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.ReturnItemRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.ReturnItemRepositories;

public class UpdateReturnItemRepository : UpdateRepository<ReturnItem>, IUpdateReturnItemRepository
{
    public UpdateReturnItemRepository(BaseDbContext context) : base(context)
    {
    }
}


using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.ReturnItemRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.ReturnItemRepositories;

public class DeleteReturnItemRepository : DeleteRepository<ReturnItem>, IDeleteReturnItemRepository
{
    public DeleteReturnItemRepository(BaseDbContext context) : base(context)
    {
    }
}

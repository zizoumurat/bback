using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.SupplierActionRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.SupplierActionRepositories;

public class DeleteSupplierActionRepository : DeleteRepository<SupplierAction>, IDeleteSupplierActionRepository
{
    public DeleteSupplierActionRepository(BaseDbContext context) : base(context)
    {
    }
}

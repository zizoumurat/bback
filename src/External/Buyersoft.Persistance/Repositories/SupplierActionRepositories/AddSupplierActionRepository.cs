using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.SupplierActionRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.SupplierActionRepositories;
public class AddSupplierActionRepository : AddRepository<SupplierAction>, IAddSupplierActionRepository
{
    public AddSupplierActionRepository(BaseDbContext context) : base(context)
    {
    }
}

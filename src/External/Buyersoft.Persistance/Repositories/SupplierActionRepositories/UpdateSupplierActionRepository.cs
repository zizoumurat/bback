using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.SupplierActionRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.SupplierActionRepositories;

public class UpdateSupplierActionRepository : UpdateRepository<SupplierAction>, IUpdateSupplierActionRepository
{
    public UpdateSupplierActionRepository(BaseDbContext context) : base(context)
    {
    }
}


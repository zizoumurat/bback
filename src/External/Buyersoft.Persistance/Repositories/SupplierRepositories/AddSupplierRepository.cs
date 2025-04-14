using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.CurrencyParameterRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.RolePermissionRepositories;
public class AddSupplierRepository : AddRepository<Supplier>, IAddSupplierRepository
{
    public AddSupplierRepository(BaseDbContext context) : base(context)
    {
    }
}

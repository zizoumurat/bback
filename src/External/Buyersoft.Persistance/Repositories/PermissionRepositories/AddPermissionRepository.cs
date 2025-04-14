using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.PermissionRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.PermissionRepositories;
public class AddPermissionRepository : AddRepository<Permission>, IAddPermissionRepository
{
    public AddPermissionRepository(BaseDbContext context) : base(context)
    {
    }
}

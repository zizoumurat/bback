using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.PermissionRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.PermissionRepositories;

public class UpdatePermissionRepository : UpdateRepository<Permission>, IUpdatePermissionRepository
{
    public UpdatePermissionRepository(BaseDbContext context) : base(context)
    {
    }
}


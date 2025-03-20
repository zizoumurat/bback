using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.RolePermissionRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.RolePermissionRepositories;

public class UpdateRolePermissionRepository : UpdateRepository<RolePermission>, IUpdateRolePermissionRepository
{
    public UpdateRolePermissionRepository(BaseDbContext context) : base(context)
    {
    }
}


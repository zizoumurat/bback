using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.RolePermissionRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.RolePermissionRepositories;

public class DeleteRolePermissionRepository : DeleteRepository<RolePermission>, IDeleteRolePermissionRepository
{
    public DeleteRolePermissionRepository(BaseDbContext context) : base(context)
    {
    }
}

using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.PermissionRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.PermissionRepositories;

public class DeletePermissionRepository : DeleteRepository<Permission>, IDeletePermissionRepository
{
    public DeletePermissionRepository(BaseDbContext context) : base(context)
    {
    }
}

using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.DepartmentRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.DepartmentRepositories;

public class UpdateDepartmentRepository : UpdateRepository<Department>, IUpdateDepartmentRepository
{
    public UpdateDepartmentRepository(BaseDbContext context) : base(context)
    {
    }
}


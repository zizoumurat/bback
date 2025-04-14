using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.DepartmentRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.DepartmentRepositories;

public class DeleteDepartmentRepository : DeleteRepository<Department>, IDeleteDepartmentRepository
{
    public DeleteDepartmentRepository(BaseDbContext context) : base(context)
    {
    }
}

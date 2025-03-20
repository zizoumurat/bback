using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.DepartmentRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.DepartmentRepositories;

public class QueryDepartmentRepository : QueryRepository<Department>, IQueryDepartmentRepository
{
    public QueryDepartmentRepository(BaseDbContext context) : base(context)
    {
    }
}

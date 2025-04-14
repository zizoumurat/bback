 using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.CategoryUserRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.CategoryUserRepositories;

public class QueryCategoryUserRepository : QueryRepository<CategoryUser>, IQueryCategoryUserRepository
{
    public QueryCategoryUserRepository(BaseDbContext context) : base(context)
    {
    }
}

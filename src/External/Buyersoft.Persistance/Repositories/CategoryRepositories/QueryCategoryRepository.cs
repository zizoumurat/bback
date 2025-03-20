using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.CategoryRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.CategoryRepositories;

public class QueryCategoryRepository : QueryRepository<Category>, IQueryCategoryRepository
{
    public QueryCategoryRepository(BaseDbContext context) : base(context)
    {
    }
}

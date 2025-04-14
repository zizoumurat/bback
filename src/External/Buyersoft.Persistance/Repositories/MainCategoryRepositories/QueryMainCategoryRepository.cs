using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.MainCategoryRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.MainCategoryRepositories;

public class QueryMainCategoryRepository : QueryRepository<MainCategory>, IQueryMainCategoryRepository
{
    public QueryMainCategoryRepository(BaseDbContext context) : base(context)
    {
    }
}

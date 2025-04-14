using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.CategoryRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.CategoryRepositories;
public class AddCategoryRepository : AddRepository<Category>, IAddCategoryRepository
{
    public AddCategoryRepository(BaseDbContext context) : base(context)
    {
    }
}

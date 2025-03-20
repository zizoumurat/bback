using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.CategoryUserRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.AddCategoryUserRepositories;
public class AddCategoryUserRepository : AddRepository<CategoryUser>, IAddCategoryUserRepository
{
    public AddCategoryUserRepository(BaseDbContext context) : base(context)
    {
    }
}

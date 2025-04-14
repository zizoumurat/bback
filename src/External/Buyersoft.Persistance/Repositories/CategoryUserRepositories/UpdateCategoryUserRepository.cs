using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.CategoryUserRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.CategoryUserRepositories;

public class UpdateCategoryUserRepository : UpdateRepository<CategoryUser>, IUpdateCategoryUserRepository
{
    public UpdateCategoryUserRepository(BaseDbContext context) : base(context)
    {
    }
}


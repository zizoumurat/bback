using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.CategoryUserRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.CategoryUserRepositories;

public class DeleteCategoryUserRepository : DeleteRepository<CategoryUser>, IDeleteCategoryUserRepository
{
    public DeleteCategoryUserRepository(BaseDbContext context) : base(context)
    {
    }
}

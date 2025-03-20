using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.MainCategoryRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.MainCategoryRepositories;

public class DeleteMainCategoryRepository : DeleteRepository<MainCategory>, IDeleteMainCategoryRepository
{
    public DeleteMainCategoryRepository(BaseDbContext context) : base(context)
    {
    }
}

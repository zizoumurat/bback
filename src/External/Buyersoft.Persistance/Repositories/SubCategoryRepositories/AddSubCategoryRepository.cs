using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.SubCategoryRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.SubCategoryRepositories;
public class AddSubCategoryRepository : AddRepository<SubCategory>, IAddSubCategoryRepository
{
    public AddSubCategoryRepository(BaseDbContext context) : base(context)
    {
    }
}

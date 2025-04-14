using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.CompanySubCategoryRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.CompanySubCategoryRepositories;

public class QueryCompanySubCategoryRepository : QueryRepository<CompanySubCategory>, IQueryCompanySubCategoryRepository
{
    public QueryCompanySubCategoryRepository(BaseDbContext context) : base(context)
    {
    }
}

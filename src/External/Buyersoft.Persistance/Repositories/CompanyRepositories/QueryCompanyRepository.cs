using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.CompanyRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.CompanyRepositories;

public class QueryCompanyRepository : QueryRepository<Company>, IQueryCompanyRepository
{
    public QueryCompanyRepository(BaseDbContext context) : base(context)
    {
    }
}

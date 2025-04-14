using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.CompanyRequestGroupRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.CompanyRequestGroupRepositories;

public class QueryCompanyRequestGroupRepository : QueryRepository<CompanyRequestGroup>, IQueryCompanyRequestGroupRepository
{
    public QueryCompanyRequestGroupRepository(BaseDbContext context) : base(context)
    {
    }
}

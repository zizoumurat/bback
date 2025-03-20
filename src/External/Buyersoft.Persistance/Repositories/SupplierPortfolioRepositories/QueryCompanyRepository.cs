using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.CompanyRepositories;
using Buyersoft.Domain.Repositories.SupplierPortfolioRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.SupplierPortfolioRepositories;

public class QueryCompanySupplierPortfolioRepository : QueryRepository<CompanySupplierPortfolio>, IQueryCompanySupplierPortfolioRepository
{
    public QueryCompanySupplierPortfolioRepository(BaseDbContext context) : base(context)
    {
    }
}

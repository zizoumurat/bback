using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.CompanyRepositories;
using Buyersoft.Domain.Repositories.SupplierPortfolioRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.SupplierPortfolioRepositories;

public class UpdateCompanySupplierPortfolioRepository : UpdateRepository<CompanySupplierPortfolio>, IUpdateCompanySupplierPortfolioRepository
{
    public UpdateCompanySupplierPortfolioRepository(BaseDbContext context) : base(context)
    {
    }
}


using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.CompanyRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.CompanyRepositories;

public class UpdateCompanyRepository : UpdateRepository<Company>, IUpdateCompanyRepository
{
    public UpdateCompanyRepository(BaseDbContext context) : base(context)
    {
    }
}


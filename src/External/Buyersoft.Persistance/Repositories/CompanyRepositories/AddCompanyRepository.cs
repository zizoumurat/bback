using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.CurrencyParameterRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.RolePermissionRepositories;
public class AddCompanyRepository : AddRepository<Company>, IAddCompanyRepository
{
    public AddCompanyRepository(BaseDbContext context) : base(context)
    {
    }
}

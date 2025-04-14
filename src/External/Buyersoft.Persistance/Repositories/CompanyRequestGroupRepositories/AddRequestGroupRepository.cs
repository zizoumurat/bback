using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.CompanyRequestGroupRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.CompanyRequestGroupRepositories;
public class AddCompanyRequestGroupRepository : AddRepository<CompanyRequestGroup>, IAddCompanyRequestGroupRepository
{
    public AddCompanyRequestGroupRepository(BaseDbContext context) : base(context)
    {
    }
}

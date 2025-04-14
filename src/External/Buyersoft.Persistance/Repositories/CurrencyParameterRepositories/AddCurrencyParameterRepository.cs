using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.CurrencyParameterRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.CurrencyParameterRepositories;
public class AddCurrencyParameterRepository : AddRepository<CurrencyParameter>, IAddCurrencyParameterRepository
{
    public AddCurrencyParameterRepository(BaseDbContext context) : base(context)
    {
    }
}

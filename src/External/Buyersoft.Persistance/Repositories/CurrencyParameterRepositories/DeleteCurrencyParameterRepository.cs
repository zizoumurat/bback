using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.CurrencyParameterRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.CurrencyParameterRepositories;

public class DeleteCurrencyParameterRepository : DeleteRepository<CurrencyParameter>, IDeleteCurrencyParameterRepository
{
    public DeleteCurrencyParameterRepository(BaseDbContext context) : base(context)
    {
    }
}

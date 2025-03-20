using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.CurrencyRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.CurrencyRepositories;

public class QueryCurrencyRepository : QueryRepository<Currency>, IQueryCurrencyRepository
{
    public QueryCurrencyRepository(BaseDbContext context) : base(context)
    {
    }
}

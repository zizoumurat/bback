using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.OfferLimitRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.OfferLimitRepositories;

public class QueryOfferLimitRepository : QueryRepository<OfferLimit>, IQueryOfferLimitRepository
{
    public QueryOfferLimitRepository(BaseDbContext context) : base(context)
    {
    }
}

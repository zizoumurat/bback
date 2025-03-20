using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.LocationRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.LocationRepositories;

public class QueryLocationRepository : QueryRepository<Location>, IQueryLocationRepository
{
    public QueryLocationRepository(BaseDbContext context) : base(context)
    {
    }
}

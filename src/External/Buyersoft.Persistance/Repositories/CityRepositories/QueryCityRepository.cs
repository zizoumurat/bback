using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.CityRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.CityRepositories;

public class QueryCityRepository : QueryRepository<City>, IQueryCityRepository
{
    public QueryCityRepository(BaseDbContext context) : base(context)
    {
    }
}

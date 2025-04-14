using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.DistrictRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.DistrictRepositories;

public class QueryDistrictRepository : QueryRepository<District>, IQueryDistrictRepository
{
    public QueryDistrictRepository(BaseDbContext context) : base(context)
    {
    }
}

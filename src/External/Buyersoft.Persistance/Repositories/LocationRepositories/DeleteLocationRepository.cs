using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.LocationRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.LocationRepositories;

public class DeleteLocationRepository : DeleteRepository<Location>, IDeleteLocationRepository
{
    public DeleteLocationRepository(BaseDbContext context) : base(context)
    {
    }
}

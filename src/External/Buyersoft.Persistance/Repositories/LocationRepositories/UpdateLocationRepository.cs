using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.LocationRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.LocationRepositories;

public class UpdateLocationRepository : UpdateRepository<Location>, IUpdateLocationRepository
{
    public UpdateLocationRepository(BaseDbContext context) : base(context)
    {
    }
}


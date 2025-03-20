using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.LocationRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.LocationRepositories;
public class AddLocationRepository : AddRepository<Location>, IAddLocationRepository
{
    public AddLocationRepository(BaseDbContext context) : base(context)
    {
    }
}

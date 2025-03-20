using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.RequestRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.RequestRepositories;

public class UpdateRequestRepository : UpdateRepository<Request>, IUpdateRequestRepository
{
    public UpdateRequestRepository(BaseDbContext context) : base(context)
    {
    }
}


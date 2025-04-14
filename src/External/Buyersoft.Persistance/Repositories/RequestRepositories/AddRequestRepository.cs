using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.RequestRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.RequestRepositories;
public class AddRequestRepository : AddRepository<Request>, IAddRequestRepository
{
    public AddRequestRepository(BaseDbContext context) : base(context)
    {
    }
}

using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.OfferLimitRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.OfferLimitRepositories;
public class AddOfferLimitRepository : AddRepository<OfferLimit>, IAddOfferLimitRepository
{
    public AddOfferLimitRepository(BaseDbContext context) : base(context)
    {
    }
}

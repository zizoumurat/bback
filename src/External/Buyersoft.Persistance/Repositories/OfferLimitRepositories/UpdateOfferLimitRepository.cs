using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.OfferLimitRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.OfferLimitRepositories;

public class UpdateOfferLimitRepository : UpdateRepository<OfferLimit>, IUpdateOfferLimitRepository
{
    public UpdateOfferLimitRepository(BaseDbContext context) : base(context)
    {
    }
}


using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.OfferLimitRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.OfferLimitRepositories;

public class DeleteOfferLimitRepository : DeleteRepository<OfferLimit>, IDeleteOfferLimitRepository
{
    public DeleteOfferLimitRepository(BaseDbContext context) : base(context)
    {
    }
}

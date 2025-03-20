using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.OfferRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.OfferRepositories;

public class UpdateOfferRepository : UpdateRepository<Offer>, IUpdateOfferRepository
{
    public UpdateOfferRepository(BaseDbContext context) : base(context)
    {
    }
}


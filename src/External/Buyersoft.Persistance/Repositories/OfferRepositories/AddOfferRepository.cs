using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.OfferRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.OfferRepositories;
public class AddOfferRepository : AddRepository<Offer>, IAddOfferRepository
{
    public AddOfferRepository(BaseDbContext context) : base(context)
    {
    }
}

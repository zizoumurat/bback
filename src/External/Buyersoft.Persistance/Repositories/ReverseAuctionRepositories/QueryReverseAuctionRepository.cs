using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.ReverseAuctionRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.ReverseAuctionRepositories;

public class QueryReverseAuctionRepository : QueryRepository<ReverseAuction>, IQueryReverseAuctionRepository
{
    public QueryReverseAuctionRepository(BaseDbContext context) : base(context)
    {
    }
}

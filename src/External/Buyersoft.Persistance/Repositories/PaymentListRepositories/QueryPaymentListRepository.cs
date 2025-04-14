using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.PaymentListRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.PaymentListRepositories;

public class QueryPaymentListRepository : QueryRepository<PaymentList>, IQueryPaymentListRepository
{
    public QueryPaymentListRepository(BaseDbContext context) : base(context)
    {
    }
}

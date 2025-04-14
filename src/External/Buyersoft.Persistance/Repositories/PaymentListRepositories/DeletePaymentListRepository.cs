using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.PaymentListRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.PaymentListRepositories;

public class DeletePaymentListRepository : DeleteRepository<PaymentList>, IDeletePaymentListRepository
{
    public DeletePaymentListRepository(BaseDbContext context) : base(context)
    {
    }
}

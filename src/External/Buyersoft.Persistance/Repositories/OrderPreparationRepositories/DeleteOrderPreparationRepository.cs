using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.OrderPreparationRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.OrderPreparationRepositories;

public class DeleteOrderPreparationRepository : DeleteRepository<OrderPreparation>, IDeleteOrderPreparationRepository
{
    public DeleteOrderPreparationRepository(BaseDbContext context) : base(context)
    {
    }
}

using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.OrderPreparationRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.OrderPreparationRepositories;

public class UpdateOrderPreparationRepository : UpdateRepository<OrderPreparation>, IUpdateOrderPreparationRepository
{
    public UpdateOrderPreparationRepository(BaseDbContext context) : base(context)
    {
    }
}


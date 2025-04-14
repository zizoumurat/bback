using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.OrderPreparationRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.OrderPreparationRepositories;
public class AddOrderPreparationRepository : AddRepository<OrderPreparation>, IAddOrderPreparationRepository
{
    public AddOrderPreparationRepository(BaseDbContext context) : base(context)
    {
    }
}

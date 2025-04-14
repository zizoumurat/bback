using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.Generic;

namespace Buyersoft.Domain.Repositories.OrderRepositories;

public interface IQueryOrderRepository : IQueryRepository<Order>
{
}
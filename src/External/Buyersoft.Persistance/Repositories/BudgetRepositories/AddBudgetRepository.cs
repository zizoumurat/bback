using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.BudgetRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.BudgetRepositories;
public class AddBudgetRepository : AddRepository<Budget>, IAddBudgetRepository
{
    public AddBudgetRepository(BaseDbContext context) : base(context)
    {
    }
}

using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.ContractRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.ContractRepositories;

public class QueryContractRepository : QueryRepository<Contract>, IQueryContractRepository
{
    public QueryContractRepository(BaseDbContext context) : base(context)
    {
    }
}

using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.BankInfoRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.BankInfoRepositories;

public class QueryBankInfoRepository : QueryRepository<BankInfo>, IQueryBankInfoRepository
{
    public QueryBankInfoRepository(BaseDbContext context) : base(context)
    {
    }
}

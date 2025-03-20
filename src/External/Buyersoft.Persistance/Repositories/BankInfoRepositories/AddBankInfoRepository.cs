using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.BankInfoRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.BankInfoRepositories;
public class AddBankInfoRepository : AddRepository<BankInfo>, IAddBankInfoRepository
{
    public AddBankInfoRepository(BaseDbContext context) : base(context)
    {
    }
}

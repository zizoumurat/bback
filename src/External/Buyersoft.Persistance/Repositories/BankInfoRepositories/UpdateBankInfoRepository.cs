using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.BankInfoRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.BankInfoRepositories;

public class UpdateBankInfoRepository : UpdateRepository<BankInfo>, IUpdateBankInfoRepository
{
    public UpdateBankInfoRepository(BaseDbContext context) : base(context)
    {
    }
}


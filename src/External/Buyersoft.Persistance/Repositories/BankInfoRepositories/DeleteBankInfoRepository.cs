using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.BankInfoRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.BankInfoRepositories;

public class DeleteBankInfoRepository : DeleteRepository<BankInfo>, IDeleteBankInfoRepository
{
    public DeleteBankInfoRepository(BaseDbContext context) : base(context)
    {
    }
}

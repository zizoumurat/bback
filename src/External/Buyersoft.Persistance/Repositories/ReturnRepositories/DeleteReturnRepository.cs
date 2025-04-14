using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.ReturnRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.ReturnRepositories;

public class DeleteReturnRepository : DeleteRepository<Return>, IDeleteReturnRepository
{
    public DeleteReturnRepository(BaseDbContext context) : base(context)
    {
    }
}

using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.ReturnRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.ReturnRepositories;

public class UpdateReturnRepository : UpdateRepository<Return>, IUpdateReturnRepository
{
    public UpdateReturnRepository(BaseDbContext context) : base(context)
    {
    }
}


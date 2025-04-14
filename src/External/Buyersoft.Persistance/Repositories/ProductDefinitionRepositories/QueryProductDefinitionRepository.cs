using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.ProductDefinitionRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.ProductDefinitionRepositories;

public class QueryProductDefinitionRepository : QueryRepository<ProductDefinition>, IQueryProductDefinitionRepository
{
    public QueryProductDefinitionRepository(BaseDbContext context) : base(context)
    {
    }
}

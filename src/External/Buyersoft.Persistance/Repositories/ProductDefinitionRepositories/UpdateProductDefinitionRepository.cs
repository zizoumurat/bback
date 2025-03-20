using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.ProductDefinitionRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.ProductDefinitionRepositories;

public class UpdateProductDefinitionRepository : UpdateRepository<ProductDefinition>, IUpdateProductDefinitionRepository
{
    public UpdateProductDefinitionRepository(BaseDbContext context) : base(context)
    {
    }
}


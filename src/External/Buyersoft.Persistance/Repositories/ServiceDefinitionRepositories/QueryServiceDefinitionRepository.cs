using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.ServiceDefinitionRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.ServiceDefinitionRepositories;

public class QueryServiceDefinitionRepository : QueryRepository<ServiceDefinition>, IQueryServiceDefinitionRepository
{
    public QueryServiceDefinitionRepository(BaseDbContext context) : base(context)
    {
    }
}

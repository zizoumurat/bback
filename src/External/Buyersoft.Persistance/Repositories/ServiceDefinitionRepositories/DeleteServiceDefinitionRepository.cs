using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.ServiceDefinitionRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.ServiceDefinitionRepositories;

public class DeleteServiceDefinitionRepository : DeleteRepository<ServiceDefinition>, IDeleteServiceDefinitionRepository
{
    public DeleteServiceDefinitionRepository(BaseDbContext context) : base(context)
    {
    }
}

using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.ServiceDefinitionRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.ServiceDefinitionRepositories;
public class AddServiceDefinitionRepository : AddRepository<ServiceDefinition>, IAddServiceDefinitionRepository
{
    public AddServiceDefinitionRepository(BaseDbContext context) : base(context)
    {
    }
}

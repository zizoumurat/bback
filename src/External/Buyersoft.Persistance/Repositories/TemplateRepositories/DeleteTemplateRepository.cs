using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.TemplateRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.TemplateRepositories;

public class DeleteTemplateRepository : DeleteRepository<Template>, IDeleteTemplateRepository
{
    public DeleteTemplateRepository(BaseDbContext context) : base(context)
    {
    }
}

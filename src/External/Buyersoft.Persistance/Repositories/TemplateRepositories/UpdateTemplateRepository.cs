using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.TemplateRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.TemplateRepositories;

public class UpdateTemplateRepository : UpdateRepository<Template>, IUpdateTemplateRepository
{
    public UpdateTemplateRepository(BaseDbContext context) : base(context)
    {
    }
}


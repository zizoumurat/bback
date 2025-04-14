using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.TemplateRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.TemplateRepositories;

public class QueryTemplateRepository : QueryRepository<Template>, IQueryTemplateRepository
{
    public QueryTemplateRepository(BaseDbContext context) : base(context)
    {
    }
}

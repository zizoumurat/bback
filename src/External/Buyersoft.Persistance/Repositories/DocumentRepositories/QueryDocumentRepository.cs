using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.DocumentRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.DocumentRepositories;

public class QueryDocumentRepository : QueryRepository<Document>, IQueryDocumentRepository
{
    public QueryDocumentRepository(BaseDbContext context) : base(context)
    {
    }
}

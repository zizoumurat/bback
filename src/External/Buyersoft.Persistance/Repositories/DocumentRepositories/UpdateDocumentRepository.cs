using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.DocumentRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.DocumentRepositories;

public class UpdateDocumentRepository : UpdateRepository<Document>, IUpdateDocumentRepository
{
    public UpdateDocumentRepository(BaseDbContext context) : base(context)
    {
    }
}


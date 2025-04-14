using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.DocumentRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.DocumentRepositories;

public class DeleteDocumentRepository : DeleteRepository<Document>, IDeleteDocumentRepository
{
    public DeleteDocumentRepository(BaseDbContext context) : base(context)
    {
    }
}

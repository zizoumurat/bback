using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.TaxOfficeRepositories;
using Buyersoft.Persistance.Context;
using Buyersoft.Persistance.Repositories.Generic;

namespace Buyersoft.Persistance.Repositories.TaxOfficeRepositories;

public class QueryTaxOfficeRepository : QueryRepository<TaxOffice>, IQueryTaxOfficeRepository
{
    public QueryTaxOfficeRepository(BaseDbContext context) : base(context)
    {
    }
}

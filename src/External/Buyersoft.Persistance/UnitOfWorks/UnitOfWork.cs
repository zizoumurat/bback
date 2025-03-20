using Buyersoft.Domain.UnitOfWorks;
using Buyersoft.Persistance.Context;

namespace Buyersoft.Persistance.UnitOfWorks;
public sealed class UnitOfWork : IUnitOfWork
{
    private readonly BaseDbContext _context;

    public UnitOfWork(BaseDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        int count = await _context.SaveChangesAsync(cancellationToken);

        return count;
    }
}

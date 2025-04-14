using Buyersoft.Domain.UnitOfWorks;
using Buyersoft.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.UnitOfWorks;

public class TransactionManager : ITransactionManager
{
    private readonly BaseDbContext _context;

    public TransactionManager(BaseDbContext context)
    {
        _context = context;
    }

    public async Task BeginTransactionAsync()
    {
        await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
        await _context.Database.CommitTransactionAsync();
    }

    public async Task RollbackAsync()
    {
        await _context.Database.RollbackTransactionAsync();
    }

    public void Dispose()
    {
        if (_context.Database.CurrentTransaction != null)
        {
            _context.Database.CurrentTransaction.Dispose();
        }
    }
}
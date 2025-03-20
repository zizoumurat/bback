using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.Generic;
using Buyersoft.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Buyersoft.Persistance.Repositories.Generic;
public class QueryRepository<T> : IQueryRepository<T> where T : BaseEntity
{
    public readonly BaseDbContext _context;
    private readonly DbSet<T> _dbSet;

    public QueryRepository(BaseDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    private static readonly Func<DbContext, int, Task<T>> GetByIdCompiled =
            EF.CompileAsyncQuery((DbContext context, int id) =>
            context.Set<T>().FirstOrDefault(p => p.Id == id));

    private static readonly Func<DbContext, int, Task<T>> GetByIdCompiledWithOutTracking =
        EF.CompileAsyncQuery((DbContext context, int id) =>
        context.Set<T>().AsNoTracking().FirstOrDefault(p => p.Id == id));

    private static readonly Func<DbContext, Expression<Func<T, bool>>, Task<T>> GetFirstCompiled =
       EF.CompileAsyncQuery((DbContext context, Expression<Func<T, bool>> expression) => context.Set<T>().FirstOrDefault(expression));

    private static readonly Func<DbContext, Expression<Func<T, bool>>, Task<T>> GetFirstAsyncWithoutTracking =
        async (DbContext context, Expression<Func<T, bool>> expression) =>
            await context.Set<T>().AsNoTracking().FirstOrDefaultAsync(expression);

    public async Task<T> GetByIdAsync(int id, bool isTracking = false)
    {
        if (isTracking)
        {
            return await GetByIdCompiled(_context, id);
        }
        else
        {
            return await GetByIdCompiledWithOutTracking(_context, id);
        }
    }

    public IQueryable<T> GetFirstAsync(Expression<Func<T, bool>> expression, bool isTracking = false)
    {
        if (isTracking)
        {
            return _dbSet.Where(expression);
        }
        else
        {
            return _dbSet.AsNoTracking().Where(expression);
        }
    }

    public IQueryable<T> GetList(Expression<Func<T, bool>> expression, bool isTracking = false)
    {
        if (isTracking)
        {
            return _dbSet.Where(expression);
        }
        else
        {
            return _dbSet.AsNoTracking().Where(expression);
        }
    }

    public async Task<bool> IsExisting(Expression<Func<T, bool>> expression)
    {
        return await _dbSet.AnyAsync(expression);
    }
}

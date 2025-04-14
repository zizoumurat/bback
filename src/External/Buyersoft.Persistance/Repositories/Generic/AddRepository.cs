using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.Generic;
using Buyersoft.Persistance.Context;

namespace Buyersoft.Persistance.Repositories.Generic;
public class AddRepository<T> : IAddRepository<T>
        where T : BaseEntity, new()

{
    private readonly BaseDbContext _context;

    public AddRepository(BaseDbContext context)
    {
        _context = context;
    }

    public async Task<T> AddAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<int> AddRangeAsync(IList<T> entities)
    {
        if (entities == null || entities.Count == 0)
        {
            throw new ArgumentNullException(nameof(entities));
        }

        await _context.Set<T>().AddRangeAsync(entities);
        await _context.SaveChangesAsync();

        return entities.Count;
    }
}

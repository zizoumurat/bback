using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.Generic;
using Buyersoft.Persistance.Context;

namespace Buyersoft.Persistance.Repositories.Generic;
public class UpdateRepository<T> : IUpdateRepository<T>
        where T : BaseEntity, new()
{
    private readonly BaseDbContext _context;

    public UpdateRepository(BaseDbContext context)
    {
        _context = context;
    }

    public T Update(T entity)
    {

        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _context.Set<T>().Update(entity);
        _context.SaveChanges();

        return entity;
    }

    public int UpdateRange(IList<T> entities)
    {
        if (entities == null || entities.Count == 0)
        {
            throw new ArgumentNullException(nameof(entities));
        }

        _context.Set<T>().UpdateRange(entities);
        _context.SaveChanges();
        
        return entities.Count;
    }
}

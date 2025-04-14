using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Entitites.Base;
using Buyersoft.Domain.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.Repositories.Generic;
public class DeleteRepository<T> : IDeleteRepository<T>
        where T : BaseEntity, new()
{

    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public DeleteRepository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public void Remove(T entity)
    {
        if (entity is SoftDeletableEntity softDeleteEntity)
        {
            softDeleteEntity.IsDeleted = true;
            _context.Update(softDeleteEntity);
        }
        else
        {
            _dbSet.Remove(entity);
        }

        _context.SaveChanges();
    }

    public void RemoveById(int id)
    {

        if (typeof(SoftDeletableEntity).IsAssignableFrom(typeof(T)))
        {
            var entity = Activator.CreateInstance(typeof(T));

            if (entity is SoftDeletableEntity softDeleteEntity)
            {
                softDeleteEntity.Id = id;
                _context.Attach(softDeleteEntity);
                softDeleteEntity.IsDeleted = true;

                _context.Entry(softDeleteEntity).Property(x => x.IsDeleted).IsModified = true;
            }
        }
        else
        {
            var entity = new T { Id = id };

            _dbSet.Remove(entity);
        }

        _context.SaveChanges();
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            if (entity is SoftDeletableEntity softDeleteEntity)
            {
                softDeleteEntity.IsDeleted = true;
                _context.Update(softDeleteEntity);
            }
            else
            {
                _dbSet.Remove(entity);
            }
        }

        _context.SaveChanges();
    }
}

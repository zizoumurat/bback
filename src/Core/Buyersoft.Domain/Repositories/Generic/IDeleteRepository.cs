using Buyersoft.Domain.Entitites;

namespace Buyersoft.Domain.Repositories.Generic;
public interface IDeleteRepository<T> where T : BaseEntity, new()
{
    void RemoveById(int id);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}

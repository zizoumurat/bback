using Buyersoft.Domain.Entitites;

namespace Buyersoft.Domain.Repositories.Generic;
public interface IAddRepository<T> where T : BaseEntity, new()
{
    Task<T> AddAsync(T entity);
    Task<int> AddRangeAsync(IList<T> entities);
}

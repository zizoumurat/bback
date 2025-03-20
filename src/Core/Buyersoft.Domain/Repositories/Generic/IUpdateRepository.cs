using Buyersoft.Domain.Entitites;

namespace Buyersoft.Domain.Repositories.Generic;

public interface IUpdateRepository<T> where T : BaseEntity, new()
{
    T Update(T entity);
    int UpdateRange(IList<T> entities);
}

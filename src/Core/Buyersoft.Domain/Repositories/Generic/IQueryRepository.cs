using Buyersoft.Domain.Entitites;
using System.Linq.Expressions;

namespace Buyersoft.Domain.Repositories.Generic;

public interface IQueryRepository<T> where T : BaseEntity
{
    IQueryable<T> GetList(Expression<Func<T, bool>> expression, bool isTracking = false);
    Task<T> GetByIdAsync(int id, bool isTracking = false);
    IQueryable<T> GetFirstAsync(Expression<Func<T, bool>> expression, bool isTracking = false);
    Task<bool> IsExisting(Expression<Func<T, bool>> expression);
}

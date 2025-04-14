namespace Buyersoft.Domain.UnitOfWorks;

public interface ITransactionManager : IDisposable
{
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}
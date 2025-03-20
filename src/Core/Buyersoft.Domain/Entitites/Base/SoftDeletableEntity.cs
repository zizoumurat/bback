namespace Buyersoft.Domain.Entitites.Base;

public interface SoftDeletableEntity
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
}

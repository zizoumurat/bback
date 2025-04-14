namespace Buyersoft.Domain.Entitites;

public class SupplierRequestGroup : BaseEntity
{
    public int SupplierId { get; set; }
    public virtual Supplier Supplier { get; set; }

    public int RequestGroupId { get; set; }
    public virtual RequestGroup RequestGroup { get; set; }
}

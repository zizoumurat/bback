namespace Buyersoft.Domain.Entitites;

public class CompanySupplierPortfolio : BaseEntity
{
    public int CompanyId { get; set; }
    public virtual Company Company { get; set; }

    public int SupplierId { get; set; }
    public virtual Supplier Supplier { get; set; }
}

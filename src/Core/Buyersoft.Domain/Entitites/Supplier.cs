namespace Buyersoft.Domain.Entitites;

public class Supplier : BaseEntity
{
    public string SupplierCode { get; set; }

    public int CompanyId { get; set; }
    public virtual Company Company { get; set; }
    public virtual ICollection<SupplierRating> SupplierRatings { get; set; }
    public virtual ICollection<SupplierRequestGroup> SupplierRequestGroups { get; set; }
    public virtual ICollection<SupplierAction> SupplierActions { get; set; }

}

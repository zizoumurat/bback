using Buyersoft.Domain.Entitites.Identity;
using Buyersoft.Domain.Enums;

namespace Buyersoft.Domain.Entitites;

public class SupplierAction : BaseEntity
{
    public int SupplierId { get; set; }
    public int CompanyId { get; set; }
    public int UserId { get; set; }
    public SupplierActionTypeEnum Type { get; set; }
    public string Subject { get; set; }
    public string Detail { get; set; }
    public string SupplierNotes { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public SupplierActionStatusEnum SupplierActionStatus { get; set; }

    public virtual Supplier Supplier { get; set; }
    public virtual Company Company { get; set; }
    public virtual User User { get; set; }
}
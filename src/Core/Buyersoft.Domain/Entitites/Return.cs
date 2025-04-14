using Buyersoft.Domain.Enums;

namespace Buyersoft.Domain.Entitites;

public class Return : BaseEntity
{
    public int OrderId { get; set; }
    public virtual Order Order { get; set; }

    public string InvoiceNumber { get; set; }
    public string WaybillNumber { get; set; }
    public string Reason { get; set; }
    public DateTime ReturnDate { get; set; }
    public decimal TotalPrice { get; set; }
    public ApprovalStatus ApprovalStatus { get; set; }

    public virtual ICollection<ReturnItem> ReturnItems { get; set; }
}

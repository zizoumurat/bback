using Buyersoft.Domain.Enums;

namespace Buyersoft.Domain.Entitites;

public class PaymentList : BaseEntity
{
    public int CompanyId { get; set; }
    public virtual Company Company { get; set; }

    public string PaymentListCode { get; set; }
    public decimal TotalPrice { get; set; }
    public string Subject { get; set; }
    public ApprovalStatus Status { get; set; }

    public virtual ICollection<PaymentListApproval> PaymentListApprovals { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
}

using Buyersoft.Domain.Entitites.Identity;
using Buyersoft.Domain.Enums;

namespace Buyersoft.Domain.Entitites;

public class PaymentListApproval : BaseEntity
{
    public int PaymentListId { get; set; }
    public int UserId { get; set; }
    public string Comment { get; set; }
    public ApprovalStatus Status { get; set; }

    public virtual User User { get; set; }
    public virtual PaymentList PaymentList { get; set; }
}
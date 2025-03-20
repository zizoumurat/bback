using Buyersoft.Domain.Entitites.Identity;
using Buyersoft.Domain.Enums;

namespace Buyersoft.Domain.Entitites;

public class Approval : BaseEntity
{
    public int RequestId { get; set; }
    public int UserId { get; set; }
    public string Comment { get; set; }
    public ApprovalStatus Status { get; set; }

    public virtual User User { get; set; }
    public virtual Request Request { get; set; }
}

public class ContractApproval : BaseEntity
{
    public int ContractId { get; set; }
    public int UserId { get; set; }
    public string Comment { get; set; }
    public ApprovalStatus Status { get; set; }

    public virtual User User { get; set; }
    public virtual Contract Contract { get; set; }
}

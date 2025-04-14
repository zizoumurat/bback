using Buyersoft.Domain.Entitites.Identity;

namespace Buyersoft.Domain.Entitites;

public class ApprovalChainUser : BaseEntity
{
    public int ApprovalChainId { get; set; }
    public int UserId { get; set; }

    public virtual User User { get; set; }
    public virtual ApprovalChain ApprovalChain { get; set; }
}
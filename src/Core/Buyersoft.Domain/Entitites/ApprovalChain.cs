namespace Buyersoft.Domain.Entitites;

public class ApprovalChain : BaseEntity
{
    public int CurrencyId { get; set; }
    public int CompanyId { get; set; }
    public decimal SpendLimit { get; set; }

    public virtual Company Company { get; set; }
    public virtual Currency Currency { get; set; }
    public virtual ICollection<ApprovalChainUser> ApprovalChainUsers { get; set; }
}

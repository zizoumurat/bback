using Buyersoft.Domain.Entitites.Identity;
using Buyersoft.Domain.Enums;

namespace Buyersoft.Domain.Entitites;

public class Contract : BaseEntity
{
    public int CompanyId { get; set; }
    public virtual Company Company { get; set; }

    public int RequestId { get; set; }
    public virtual Request Request { get; set; }

    public int? DocumentId { get; set; }
    public virtual Document Document { get; set; }

    public int OfferId { get; set; }
    public virtual Offer Offer { get; set; }

    public string ReferenceCode { get; set; }
    public decimal TotalPrice { get; set; }
    public ContractStatus ContractStatus { get; set; }
    public string RejectionReason { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime ExpirationDate { get; set; }

    public virtual ICollection<ContractComment> ContractComments { get; set; }
    public virtual ICollection<ContractApproval> ContractApprovals { get; set; }
}

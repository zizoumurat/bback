using Buyersoft.Domain.Entitites.Identity;
using Buyersoft.Domain.Enums;

namespace Buyersoft.Domain.Entitites;
public class Request : BaseEntity
{
    public int CompanyId { get; set; }
    public string Title { get; set; }
    public string RequestCode { get; set; }
    public decimal Amount { get; set; }
    public DateTime RequestedSupplyDate { get; set; }
    public DateTime? EstimatedSupplyDate { get; set; }
    public string Reason { get; set; }
    public int CurrencyId { get; set; }
    public int CategoryId { get; set; }
    public int TemplateId { get; set; }
    public int CreatedById { get; set; }
    public User CreatedBy { get; set; }
    public int? ManagerId { get; set; }
    public User Manager { get; set; }
    public int? BudgetId { get; set; }
    public bool BudgetInclusionStatus { get; set; }
    public DateTime? ApprovedDate { get; set; }
    public CollectionChannel? CollectionChannel { get; set; }
    public RequestStateEnum State { get; set; }
    public string CancellationReason { get; set; }
    public DateTime? BidCollectionEndDate { get; set; }
    public int? ReverseAuctionId { get; set; }
    public string CommercialEvaluation { get; set; } = string.Empty;
    public string TechnicalEvaluation { get; set; } = string.Empty;
    public ReverseAuction ReverseAuction { get; set; }

    public virtual Company Company { get; set; }
    public virtual Category Category { get; set; }
    public virtual Currency Currency { get; set; }
    public virtual Template Template { get; set; }
    public virtual Budget Budget { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
    public virtual ICollection<Contract> Contracts { get; set; }
    public virtual ICollection<RequestDocument> RequestDocuments { get; set; }
    public virtual ICollection<Offer> Offers { get; set; }
    public virtual ICollection<Approval> Approvals { get; set; }
    public virtual ICollection<OrderPreparation> OrderPreparations { get; set; }
}

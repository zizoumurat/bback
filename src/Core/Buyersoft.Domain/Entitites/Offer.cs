using Buyersoft.Domain.Enums;

namespace Buyersoft.Domain.Entitites;

public class Offer : BaseEntity
{
    public int CompanyId { get; set; }
    public virtual Company Company { get; set; }

    public int RequestId { get; set; }
    public virtual Request Request { get; set; }

    public int? DocumentId { get; set; }
    public virtual Document Document { get; set; }

    public int? RevisedOfferId { get; set; }
    public virtual Offer RevisedOffer { get; set; }

    public int? OriginalOfferId { get; set; }
    public Offer OriginalOffer { get; set; }

    public string ReferenceCode { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal AverageUnitPrice { get; set; }
    public string Notes { get; set; }
    public int? MaturityDays { get; set; }
    public bool AddedToShortList { get; set; }
    public bool AddedToComparisonTable { get; set; }
    public bool AddedToReverseAuction { get; set; }
    public bool IsRevised { get; set; }
    public bool IsOptional { get; set; }
    public bool IsSelected { get; set; }
    public OfferStatus OfferStatus { get; set; }
    public string RejectionReason { get; set; }
    public DateTime OfferDate { get; set; }

    public ParticipationStatusEnum AuctionParticipationStatus { get; set; }

    public List<OfferDetail> OfferDetails { get; set; }
    public DateTime ExpirationDate { get; set; }

    public virtual Contract Contract { get; set; }
    public virtual ICollection<OrderPreparation> OrderPreparations { get; set; }
}

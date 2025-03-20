namespace Buyersoft.Domain.Entitites;

public class OfferDetail : BaseEntity
{
    public int OfferId { get; set; }
    public Offer Offer { get; set; }

    public string ProductDefinition { get; set; }
    public decimal FirstUnitPrice { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public int Quantity { get; set; }
    public int? Allocation { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; }
}

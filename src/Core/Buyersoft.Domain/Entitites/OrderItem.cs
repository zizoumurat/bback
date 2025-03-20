namespace Buyersoft.Domain.Entitites;

public class OrderItem : BaseEntity
{
    public int OrderId { get; set; }
    public virtual Order Order { get; set; }

    public int OfferDetailId { get; set; }
    public virtual OfferDetail OfferDetail { get; set; }

    public string ProductDefinition { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public int Quantity { get; set; }
}
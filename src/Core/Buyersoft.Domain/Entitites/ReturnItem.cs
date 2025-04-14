namespace Buyersoft.Domain.Entitites;

public class ReturnItem : BaseEntity
{
    public int ReturnId { get; set; }
    public virtual Return Return { get; set; }

    public int OrderItemId { get; set; }
    public virtual OrderItem OrderItem { get; set; }

    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public int Quantity { get; set; }
}
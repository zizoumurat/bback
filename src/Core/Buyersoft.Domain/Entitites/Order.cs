using Buyersoft.Domain.Enums;

namespace Buyersoft.Domain.Entitites;

public class Order: BaseEntity
{
    public DateTime OrderDate { get; set; }
    public string OrderCode { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderStatusEnum Status { get; set; }

    public int OrderPreparationId { get; set; }
    public virtual OrderPreparation OrderPreparation { get; set; }

    public OrderStatusEnum? NonconformityStatus { get; set; } 
    public NonconformityReasonEnum? NonconformityReason { get; set; } 
    public string NonconformityDetail { get; set; } 
    public string CompanyComments { get; set; } 
    public string SupplierComments { get; set; } 


    public virtual ICollection<OrderItem> OrderItems { get; set; }
}

namespace Buyersoft.Domain.Entitites;

public class OrderPreparation : BaseEntity
{
    public int CompanyId { get; set; }
    public virtual Company Company { get; set; }

    public int RequestId { get; set; }
    public virtual Request Request { get; set; }

    public int OfferId { get; set; }
    public virtual Offer Offer { get; set; }
    public string MainCategory { get; set; }
    public string RequestCode { get; set; }
    public string ReferenceCode { get; set; }
    public string SubCategory { get; set; }
    public string RequestGroup { get; set; }
    public decimal TotalPrice { get; set; }
    public bool AvailableLimit { get; set; }
    public List<Order> Orders { get; set; }
}

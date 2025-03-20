namespace Buyersoft.Domain.Dtos;

public class OrderPreparationListDto()
{
    public int Id { get; set; }
    public int RequestId { get; set; }
    public int OfferId { get; set; }
    public int OrderCount { get; set; }

    public string MainCategory { get; set; }
    public string SubCategory { get; set; }
    public string RequestGroup { get; set; }
    public string RequestCode { get; set; }
    public string CurrencyCode { get; set; }
    public string ReferenceCode { get; set; }
    public decimal TotalPrice { get; set; }
    public bool AvailableLimit { get; set; }

    public string Supplier { get; set; }
    public string Unit { get; set; }

    public List<OrderListDto> Orders { get; set; }
    public List<OfferDetailListDto> OfferDetailList { get; set; }
}



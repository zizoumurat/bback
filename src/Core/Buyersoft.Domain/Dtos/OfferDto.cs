using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Buyersoft.Domain.Dtos;

public sealed record OfferDto(
    int Id,
    int CompanyId,
    int RequestId,
    string ReferenceCode,
    decimal TotalPrice,
    decimal AverageUnitPrice,
    int? MaturityDays,
    OfferStatus OfferStatus,
    string RejectionReason,
    DateTime ExpirationDate
 );

public sealed record OfferFilterDto(
    int? CategoryId,
    int? CurrencyId,
    string CurrencyName,
    string CategoryName,
    double? SpendLimitMax,
    double? SpendLimitMin,
    int? OfferMax,
    int? OfferMin
);

public class OfferListDto()
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
    public string ReferenceCode { get; set; }
    public int RequestId { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal AverageUnitPrice { get; set; }
    public int MaturityDays { get; set; }
    public OfferStatus OfferStatus { get; set; }
    public bool AddedToShortList { get; set; }
    public bool AddedToComparisonTable { get; set; }
    public bool AddedToReverseAuction { get; set; }
    public bool IsRevised { get; set; }
    public bool IsOptional { get; set; }
    public bool IsSelected { get; set; }
    public string RejectionReason { get; set; }
    public string Notes { get; set; }
    public string DocumentUrl { get; set; }
    public int? OriginalOfferId { get; set; }
    public int? RevisedOfferId { get; set; }
    public DocumentDto Document { get; set; }
    public List<OfferDetailDto> OfferDetails { get; set; }
    public DateTime OfferDate { get; set; }
    public DateTime ExpirationDate { get; set; }
}

public sealed record RejectOfferDto(int RequestId, string RejectionReason);

public sealed record MakeOfferDto(int RequestId, List<int> PriceList, int MaturityDays, string Notes, DateTime ExpirationDate, IFormFile Document = null);

public sealed record OfferDetailDto(int Id, int OfferId, decimal FirstUnitPrice, decimal UnitPrice, decimal TotalPrice, int Quantity, int? Allocation);

public sealed record UpdateOfferPriceDto
{
    public int OfferDetailId { get; set; }
    public decimal NewUnitPrice { get; set; }

    public UpdateOfferPriceDto(int offerDetailId, decimal newUnitPrice)
    {
        OfferDetailId = offerDetailId;
        NewUnitPrice = newUnitPrice;
    }
}


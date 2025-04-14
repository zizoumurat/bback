namespace Buyersoft.Domain.Dtos;

public sealed record OfferLimitDto(
    int Id,
    int CompanyId,
    int CurrencyId,
    double SpendLimit,
    int MinimumOfferCount
 );

public sealed record OfferLimitFilterDto(
    int? CurrencyId,
    double? SpendLimitMax,
    double? SpendLimitMin,
    int? MinimumOfferCountMin,
    int? MinimumOfferCountMax
);

public class OfferLimitListDto()
{
    public int Id { get; set; }
    public int CurrencyId { get; set; }
    public string CurrencyName { get; set; }
    public double SpendLimit { get; set; }
    public int MinimumOfferCount { get; set; }
}

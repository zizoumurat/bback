namespace Buyersoft.Domain.Dtos;
public record ApprovalChainDto(
    int Id,
    int CompanyId,
    int CurrencyId,
    decimal SpendLimit,
    int[] UserIdList
 );

public sealed record ApprovalChainFilterDto(
    int? CurrencyId,
    int? UserId,
    decimal? SpendLimitMax,
    decimal? SpendLimitMin
);


public class ApprovalChainListDto()
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public int CurrencyId { get; set; }
    public string CurrencyName { get; set; }
    public decimal SpendLimit { get; set; }
    public List<int> UserIdList { get; set; }
    public List<string> OwnerUserList { get; set; }
}


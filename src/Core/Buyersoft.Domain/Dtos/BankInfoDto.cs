namespace Buyersoft.Domain.Dtos;

public sealed record BankInfoDto(int Id, string IBAN, int CompanyId, int CurrencyId, int? BranchId);

public sealed record BankInfoFilterDto(
     string Currency,
     string BankName,
     string BankBranch,
     int? BranchId,
     string IBAN,
     string City,
     int? CityId,
     string District,
     int? DistrictId,
     int? CompanyId
);

public class BankInfoListDto()
{
    public int Id { get; set; }
    public string IBAN { get; set; }
    public int CompanyId { get; set; }
    public int CurrencyId { get; set; }
    public int CityId { get; set; }
    public int DistrictId { get; set; }
    public string CurrencyName { get; set; }
    public int? BranchId { get; set; }
    public string BranchName { get; set; }
    public string BankName { get; set; }
}
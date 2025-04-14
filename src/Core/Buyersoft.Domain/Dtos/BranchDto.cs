namespace Buyersoft.Domain.Dtos;

public sealed record BranchDto(int Id, string Name, int CompanyId);

public sealed record BranchFilterDto(
    string BranchName,
    string BankName,
    string Address,
    string City,
    string District,
    int? CityId,
    int? DistrictId
);

public class BranchListDto()
{
    public int Id { get; set; }
    public string BankName { get; set; }
    public string BranchName { get; set; }
    public string BranchAndBankName { get; set; }
    public string Address { get; set; }
    public string District { get; set; }
    public string City { get; set; }
    public string PhoneNumber { get; set; }
    public string FaksNumber { get; set; }
    public int CityId { get; set; }
    public int DistrictId { get; set; }
}

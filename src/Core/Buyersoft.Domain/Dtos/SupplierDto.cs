namespace Buyersoft.Domain.Dtos;

public sealed record SupplierDto(int Id, string Name, int CompanyId);

public sealed record SupplierFilterDto(
    string Code,
    string ErpCode,
    string TaxOffice,
    int? CompanyId,
    int? CategoryId,
    int? LocationId,
    int? CurrencyId,
    int? PaymentTermErpCode,
    int? Rating
);

public class SupplierListDto()
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string ErpCode { get; set; }
    public int CompanyId { get; set; }
    public int LocationId { get; set; }
    public string TaxOffice { get; set; }
    public int CategoryId { get; set; }
    public int GroupId { get; set; }
    public int PreferredId { get; set; }
    public int CurrencyId { get; set; }
    public int PaymentTermErpCode { get; set; }
    public int Rating { get; set; }
}


public class SupplierDtoForCategory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BuyersoftRating { get; set; }
    public int UserRating { get; set; }
    public string Email { get; set; }
    public string WebSite { get; set; }
    public int Channel { get; set; }
    public bool IsPreferred { get; set; }
}

public class SupplierCreateDto
{
    public string Name { get; set; }
    public string ContactFirstName { get; set; }
    public string ContactLastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string RePassword { get; set; }
    public string WebSite { get; set; }
    public string Phone { get; set; }
    public string SupplierCode { get; set; }
    public int CityId { get; set; }
    public int DistrictId { get; set; }
    public string Address { get; set; }
    public string TaxAdministration { get; set; }
    public string TaxNumber { get; set; }
    public List<int> RequestGroupIdList { get; set; }

}

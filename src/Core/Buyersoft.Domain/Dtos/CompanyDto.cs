using Microsoft.AspNetCore.Http;

namespace Buyersoft.Domain.Dtos;

public sealed record CompanyDto(
     int Id,
     string Name,
     string ContactFirstName,
     string ContactLastName,
     string ContactPhoneNumber,
     string Address,
     string Website,
     string Email,
     string Phone,
     string TaxAdministration,
     string TaxNumber,
     int CityId,
     int DistrictId,
     bool IsSupplier,
     bool IsDeleted);


public sealed record UpdateCompanyDto(
     int Id,
     string Name,
     string Address,
     string Website,
     string Email,
     string Phone,
     string TaxAdministration,
     string TaxNumber,
     int CityId,
     int DistrictId,
     IFormFile Logo);


public sealed record CompanyFilterDto(string Name);

public sealed record CompanyListDto(int Id,
     string Name,
     string ContactFirstName,
     string ContactLastName,
     string ContactPhoneNumber,
     string Address,
     string LogoUrl,
     string Website,
     string Email,
     string Phone,
     string TaxAdministration,
     string TaxNumber,
     int CityId,
     int DistrictId,
     bool IsSupplier,
     bool IsDeleted);

public class CompanyDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string LogoUrl { get; set; }
    public string Website { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int TaxAdministration { get; set; }
    public string TaxNumber { get; set; }
    public int CityId { get; set; }
    public int DistrictId { get; set; }
    public bool IsSupplier { get; set; }
    public bool IsDeleted { get; set; }
}

public class SupplierPortfolioDto
{
    public int Id { get; set; }
    public int SupplierId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string Contact { get; set; }
    public string TaxAdministration { get; set; }
}
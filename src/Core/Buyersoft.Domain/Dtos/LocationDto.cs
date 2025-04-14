namespace Buyersoft.Domain.Dtos;

public sealed record LocationDto(
     int Id,
     string Name,
     string Address,
     int CityId,
     int DistrictId,
     int CompanyId,
     int Type,
     string Latitude,
     string Longitude
 );

public sealed record LocationFilterDto(
     string Name,
     string Address,
     int? CityId,
     int? DistrictId,
     DateTime? CreatedDateStart,
     DateTime? CreatedDateEnd
 );

public class LocationListDto()
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CityId { get; set; }
    public string CityName { get; set; }
    public int DistrictId { get; set; }
    public string DistrictName { get; set; }
    public string Address { get; set; }
    public bool Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; }
}
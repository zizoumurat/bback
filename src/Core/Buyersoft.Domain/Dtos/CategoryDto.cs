namespace Buyersoft.Domain.Dtos;

public sealed record CategoryDto(
    int Id,
    int CompanyId,
    int MainCategoryId,
    int SubCategoryId,
    int RequestGroupId,
    int LocationId,
    int LeadTime,
    string Unit,
    int[] UserIdList
 );

public sealed record CategoryFindDto(int Id, string Unit, string Location);

public sealed record CategoryFilterDto(
     int? MainCategoryId,
     int? SubCategoryId,
     int? UserId,
     int? LeadTime
 );

public class CategoryListDto()
{
    public int Id { get; set; }
    public string CategoryName { get; set; }
    public int MainCategoryId { get; set; }
    public string MainCategoryName { get; set; }
    public int SubCategoryId { get; set; }
    public string SubCategoryName { get; set; }
    public string CompanySubCategoryName { get; set; }
    public string SubCategoryCode { get; set; }
    public int RequestGroupId { get; set; }
    public string RequestGroupName { get; set; }
    public string CompanyRequestGroupName { get; set; }
    public int LocationId { get; set; }
    public string LocationName { get; set; }
    public int UserId { get; set; }
    public List<string> OwnerUserList { get; set; }
    public int LeadTime { get; set; }
    public string Unit { get; set; }
    public List<int> UserIdList { get; set; }
}

public class CategoryExcellListDto()
{
    public int Id { get; set; }
    public string CategoryName { get; set; }
    public int MainCategoryId { get; set; }
    public string MainCategoryName { get; set; }
    public int SubCategoryId { get; set; }
    public string SubCategoryName { get; set; }
    public string CompanySubCategoryName { get; set; }
    public string SubCategoryCode { get; set; }
    public int RequestGroupId { get; set; }
    public string RequestGroupName { get; set; }
    public string CompanyRequestGroupName { get; set; }
    public int LocationId { get; set; }
    public string LocationName { get; set; }
    public int UserId { get; set; }
    public List<string> OwnerUserList { get; set; }
    public int LeadTime { get; set; }
    public string Unit { get; set; }
    public List<int> UserIdList { get; set; }

    public List<ProductDefinitionDto> ProductDefinitions { get; set; }
    public List<ServiceDefinitionDto> ServiceDefinitionDtos { get; set; }
}

public sealed record CategoryGroupFilter(int mainCategoryId, int subCategoryId, int requestGroupId);
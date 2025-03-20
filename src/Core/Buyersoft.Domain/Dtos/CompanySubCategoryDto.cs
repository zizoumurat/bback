namespace Buyersoft.Domain.Dtos;

public sealed record CompanySubCategoryDto(
    int Id,
    string Name,
    int SubCategoryId
);

public class CompanySubCategoryListDto()
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SubCategoryId { get; set; }
}
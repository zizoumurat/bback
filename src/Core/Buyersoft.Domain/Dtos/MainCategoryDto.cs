namespace Buyersoft.Domain.Dtos;

public sealed record MainCategoryDto(int Id, string Name, int CompanyId);

public sealed record MainCategoryFilterDto(string Name);

public class MainCategoryListDto()
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    string Name { get; set; }
}
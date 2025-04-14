namespace Buyersoft.Domain.Dtos;

public sealed record SubCategoryDto(
    int Id,
    string Name,
    int MainCategoryId
);

public class SubCategoryListDto()
{
    public int Id { get; set; }
    public string Name { get; set; }
}
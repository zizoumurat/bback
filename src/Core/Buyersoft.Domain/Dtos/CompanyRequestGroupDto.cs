namespace Buyersoft.Domain.Dtos;

public sealed record CompanyRequestGroupDto(
    int Id,
    string Name,
    int RequestGroupId,
    int CompanyId
);

public class CompanyRequestGroupListDto()
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int RequestGroupId { get; set; }
}
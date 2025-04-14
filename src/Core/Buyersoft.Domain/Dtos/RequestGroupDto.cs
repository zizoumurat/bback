namespace Buyersoft.Domain.Dtos;

public sealed record RequestGroupDto(
    int Id,
    string Name,
    int CompanyId
);

public class RequestGroupListDto()
{
    public int Id { get; set; }
    public string Name { get; set; }
}
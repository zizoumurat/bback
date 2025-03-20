namespace Buyersoft.Domain.Dtos;

public sealed record TemplateDto(int Id, int CompanyId, string Name, string Data, int RequestGroupId);

public sealed record TemplateFilterDto(string Name);

public class TemplateListDto()
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string Name { get; set; }
    public string Data { get; set; }
}
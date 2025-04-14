namespace Buyersoft.Domain.Dtos;

public sealed record DepartmentDto(int Id, string Name, int CompanyId);

public sealed record DepartmentFilterDto(string Name);

public class DepartmentListDto()
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string Name { get; set; }
}

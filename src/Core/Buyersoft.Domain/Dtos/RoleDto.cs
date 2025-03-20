namespace Buyersoft.Domain.Dtos;

public sealed record RoleDto(
    int Id,
    string Name,
    int CompanyId,
    bool IsSystemRole
 );

public sealed record RoleFilterDto(string Name);

public class RoleListDto()
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsSystemRole { get; set; }
}

public sealed record UpdateRolePermissionDto(int RoleId, List<int> PermissionIdList);


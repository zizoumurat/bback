namespace Buyersoft.Application.Dtos;

public class PermissionDto
{
    public string Name { get; set; }
}

public class ModuleDto
{
    public string Name { get; set; }
    public List<PermissionDto> PermissionList { get; set; }
}
using Buyersoft.Domain.Enums;

namespace Buyersoft.Domain.Dtos;
public class PermissionDto
{
    public int Id { get; set; }

    public string Name { get; set; }
}

public class PermissionListDto
{
    public string Name { get; set; }

    public List<PermissionDto> ActionList { get; set; }
}
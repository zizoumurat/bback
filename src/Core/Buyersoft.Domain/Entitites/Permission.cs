using Buyersoft.Domain.Enums;

namespace Buyersoft.Domain.Entitites;
public class Permission : BaseEntity
{
    public string Name { get; set; }

    public virtual List<RolePermission> RolePermissions { get; set; }
}

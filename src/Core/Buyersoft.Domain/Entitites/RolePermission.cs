using Buyersoft.Domain.Entitites.Identity;

namespace Buyersoft.Domain.Entitites;
public class RolePermission: BaseEntity
{
    public int RoleId { get; set; }
    public int PermissionId { get; set; }

    public virtual Role Role { get; set; }
    public virtual Permission Permission { get; set; }
}
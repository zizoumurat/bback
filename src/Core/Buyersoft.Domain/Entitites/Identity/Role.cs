 using Microsoft.AspNetCore.Identity;

namespace Buyersoft.Domain.Entitites.Identity;
public class Role : IdentityRole<int>
{
    public Role()
    {

    }
    public Role(string name)
    {
        Name = name;
    }

    public int? CompanyId { get; set; }
    public bool IsSystemRole { get; set; }
    public bool IsHiddenRole { get; set; }

    public virtual List<RolePermission> RolePermissions { get; set; }
    public virtual Company Company { get; set; }
    public virtual ICollection<User> Users { get; set; }
}
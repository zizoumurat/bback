using Buyersoft.Domain.Entitites.Base;
using Buyersoft.Domain.Entitites.Identity;

namespace Buyersoft.Domain.Entitites;

public class Department : SelectableEntity
{
    public int CompanyId { get; set; }

    public virtual Company Company { get; set; }
    public virtual ICollection<Budget> Budgets { get; set; }
    public virtual ICollection<User> Users { get; set; }
}

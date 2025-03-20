using Buyersoft.Domain.Entitites.Base;

namespace Buyersoft.Domain.Entitites;

public class MainCategory : SelectableEntity, SoftDeletableEntity
{
    public virtual ICollection<Category> Categories { get; set; }
    public virtual ICollection<SubCategory> SubCategories { get; set; }
    public bool IsDeleted { get; set; }
}

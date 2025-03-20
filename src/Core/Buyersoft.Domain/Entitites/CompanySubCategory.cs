using Buyersoft.Domain.Entitites.Base;

namespace Buyersoft.Domain.Entitites;

public class CompanySubCategory : SelectableEntity
{
    public int SubCategoryId { get; set; }
    public virtual SubCategory SubCategory { get; set; }

    public int CompanyId { get; set; }
    public virtual Company Company { get; set; }
}

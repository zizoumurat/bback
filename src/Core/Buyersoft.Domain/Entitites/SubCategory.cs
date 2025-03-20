using Buyersoft.Domain.Entitites.Base;

namespace Buyersoft.Domain.Entitites;

public class SubCategory : SelectableEntity
{
    public int MainCategoryId { get; set; }
    public virtual MainCategory MainCategory { get; set; }

    public virtual ICollection<Category> Categories { get; set; }
    public virtual ICollection<RequestGroup> RequestGroups { get; set; }

    public virtual CompanySubCategory CompanySubCategory { get; set; }

    public SubCategory()
    {
        Categories = new List<Category>();
        RequestGroups = new List<RequestGroup>();
    }
}

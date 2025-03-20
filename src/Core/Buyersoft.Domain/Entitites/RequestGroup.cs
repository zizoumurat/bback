using Buyersoft.Domain.Entitites.Base;

namespace Buyersoft.Domain.Entitites;

public class RequestGroup : SelectableEntity
{
    public int SubCategoryId { get; set; }
    public virtual SubCategory SubCategory { get; set; }
    public virtual ICollection<Category> Categories { get; set; }
    public virtual CompanyRequestGroup CompanyRequestGroup { get; set; }
    public virtual ICollection<SupplierRequestGroup> SupplierRequestGroups { get; set; }

    public RequestGroup()
    {
        Categories = new List<Category>();
    }
}
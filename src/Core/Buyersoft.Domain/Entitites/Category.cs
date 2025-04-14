using Buyersoft.Domain.Entitites.Base;

namespace Buyersoft.Domain.Entitites;

public class Category : BaseEntity, SoftDeletableEntity
{
    public int CompanyId { get; set; }
    public int MainCategoryId { get; set; }
    public int SubCategoryId { get; set; }
    public int RequestGroupId { get; set; }
    public int LocationId { get; set; }
    public int LeadTime { get; set; }
    public string Unit { get; set; }
    public bool IsDeleted { get; set; }

    public virtual Company Company { get; set; }
    public virtual MainCategory MainCategory { get; set; }
    public virtual SubCategory SubCategory { get; set; }
    public virtual RequestGroup RequestGroup { get; set; }
    public virtual Location Location { get; set; }
    public virtual ICollection<CategoryUser> CategoryUsers { get; set; }
    public virtual ICollection<Request> Requests { get; set; }
    public virtual ICollection<ProductDefinition> ProductDefinitions { get; set; }
    public virtual ICollection<ServiceDefinition> ServiceDefinitions { get; set; }
}

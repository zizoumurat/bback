namespace Buyersoft.Domain.Entitites;

public class ProductDefinition : BaseEntity
{
    public string Code { get; set; }

    public string Definition { get; set; }

    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
}

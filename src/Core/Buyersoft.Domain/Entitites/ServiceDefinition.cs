namespace Buyersoft.Domain.Entitites;

public class ServiceDefinition : BaseEntity
{
    public string Definition { get; set; }

    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
}


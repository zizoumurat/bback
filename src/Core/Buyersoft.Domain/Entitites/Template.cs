namespace Buyersoft.Domain.Entitites;
public class Template : BaseEntity
{
    public int CompanyId { get; set;}
    
    public string Name { get; set; }

    public string Data { get; set; }

    public int RequestGroupId { get; set; }

    public virtual ICollection<Request> Requests { get; set; }
}

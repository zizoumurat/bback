namespace Buyersoft.Domain.Entitites;

public class RequestDocument : BaseEntity
{
    public int RequestId { get; set; }
    public int DocumentId { get; set; }
    public virtual Document Document { get; set; }
    public virtual Request  Request{ get; set; }
}
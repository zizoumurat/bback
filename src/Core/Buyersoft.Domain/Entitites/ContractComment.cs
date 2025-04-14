using Buyersoft.Domain.Entitites.Identity;

namespace Buyersoft.Domain.Entitites;

public class ContractComment : BaseEntity
{
    public int ContractId { get; set; }
    public virtual Contract Contract { get; set; }

    public int UserId { get; set; }
    public virtual User User { get; set; }

    public string Comment { get; set; }
    public DateTime CommentDate { get; set; }
}
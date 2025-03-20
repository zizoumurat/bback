using Buyersoft.Domain.Entitites.Identity;

namespace Buyersoft.Domain.Entitites;

public class Comment : BaseEntity
{
    public int UserId { get; set; }
    public string CommentText { get; set; }
    public int RequestId { get; set; }

    public virtual Request Request { get; set; }
    public virtual User User { get; set; }
}

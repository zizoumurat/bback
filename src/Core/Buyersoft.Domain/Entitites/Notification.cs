using Buyersoft.Domain.Entitites.Identity;
namespace Buyersoft.Domain.Entitites;

public class Notification : BaseEntity
{
    public int UserId { get; set; }
    public string Message { get; set; }
    public bool Read { get; set; }
    public virtual User User { get; set; }
}

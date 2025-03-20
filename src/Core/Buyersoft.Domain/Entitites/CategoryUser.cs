using Buyersoft.Domain.Entitites.Identity;

namespace Buyersoft.Domain.Entitites;
public class CategoryUser : BaseEntity
{
    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}

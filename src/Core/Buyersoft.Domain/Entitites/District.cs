using Buyersoft.Domain.Entitites.Base;

namespace Buyersoft.Domain.Entitites;

public class District : SelectableEntity
{
    public int CityId { get; set; }

    public virtual City City { get; set; }
    public virtual ICollection<Branch> Branch { get; set; }
    public virtual ICollection<Location> Locations { get; set; }
    public virtual ICollection<Company> Companies { get; set; }
}
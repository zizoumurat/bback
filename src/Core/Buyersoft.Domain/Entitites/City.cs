using Buyersoft.Domain.Entitites.Base;

namespace Buyersoft.Domain.Entitites;

public class City : SelectableEntity
{
    public virtual ICollection<Branch> Branches { get; set; }
    public virtual ICollection<District> Districts { get; set; }
    public virtual ICollection<Location> Locations { get; set; }
    public virtual ICollection<TaxOffice> TaxOffices { get; set; }
    public virtual ICollection<Company> Companies { get; set; }
}
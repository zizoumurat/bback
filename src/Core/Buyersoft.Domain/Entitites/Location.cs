using Buyersoft.Domain.Entitites.Base;

namespace Buyersoft.Domain.Entitites;

public class Location : SelectableEntity
{
    public string Address { get; set; }
    public int CityId { get; set; }
    public int DistrictId { get; set; }
    public int CompanyId { get; set; }

    public virtual City City { get; set; }
    public virtual District District { get; set; }
    public virtual Company Company { get; set; }
    public virtual ICollection<Category> Categories { get; set; }

}

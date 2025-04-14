using Buyersoft.Domain.Entitites.Base;

namespace Buyersoft.Domain.Entitites;

public class TaxOffice : SelectableEntity
{
    public int Code { get; set; }
    public int CityId { get; set; }
    public virtual City City { get; set; }
}

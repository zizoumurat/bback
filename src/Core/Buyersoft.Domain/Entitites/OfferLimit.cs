namespace Buyersoft.Domain.Entitites;

public class OfferLimit : BaseEntity
{
    public int CompanyId { get; set; }
    public int CurrencyId { get; set; }
    public double SpendLimit { get; set; }
    public int MinimumOfferCount { get; set; }
    public virtual Company Company { get; set; }
    public virtual Currency Currency { get; set; }
}

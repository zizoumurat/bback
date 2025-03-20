namespace Buyersoft.Domain.Entitites;

public class CurrencyParameter : BaseEntity
{
    public int Currency1Id { get; set; }
    public int Currency2Id { get; set; }
    public int CompanyId { get; set; }
    public decimal ExchangeRate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime ExpiredDate { get; set; }

    public virtual Currency Currency1 { get; set; }
    public virtual Currency Currency2 { get; set; }
    public virtual Company Company { get; set; }
}

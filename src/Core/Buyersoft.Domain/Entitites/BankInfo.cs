namespace Buyersoft.Domain.Entitites;

public class BankInfo : BaseEntity
{
    public string IBAN { get; set; }
    public int CompanyId { get; set; }
    public int CurrencyId { get; set; }
    public int? BranchId { get; set; }

    public virtual Company Company { get; set; }
    public virtual Branch Branch { get; set; }
    public virtual Currency Currency { get; set; }
}
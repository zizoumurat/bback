using Buyersoft.Domain.Entitites.Base;

namespace Buyersoft.Domain.Entitites;

public class Currency : SelectableEntity
{
    public string Code { get; set; }
    
    public virtual ICollection<BankInfo> BankInfos { get; set; }
    public virtual ICollection<ApprovalChain> ApprovalChains { get; set; }
    public virtual ICollection<CurrencyParameter> ExchangeRatesCurrency1 { get; set; }
    public virtual ICollection<CurrencyParameter> ExchangeRatesCurrency2 { get; set; }
    public virtual ICollection<OfferLimit> OfferLimits { get; set; }
    public virtual ICollection<Budget> Budgets { get; set; }
}

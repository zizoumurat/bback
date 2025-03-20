using Buyersoft.Domain.Entitites.Base;

namespace Buyersoft.Domain.Entitites;

public class Budget : BaseEntity, SoftDeletableEntity
{
    public int CompanyId { get; set; }
    public int CurrencyId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int DepartmentId { get; set; }
    public decimal BudgetLimit { get; set; }
    public decimal AvailableLimit { get; set; }
    public string BudgetTitle { get; set; }
    public bool IsDeleted { get; set; }

    public virtual Company Company { get; set; }
    public virtual Department Department { get; set; }
    public virtual Currency Currency { get; set; }
    public virtual ICollection<Request> Requests { get; set; }
}

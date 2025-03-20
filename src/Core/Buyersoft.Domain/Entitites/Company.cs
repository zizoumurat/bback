using Buyersoft.Domain.Entitites.Base;
using Buyersoft.Domain.Entitites.Identity;

namespace Buyersoft.Domain.Entitites;

public class Company : BaseEntity, SoftDeletableEntity
{
    public string Name { get; set; }
    public string ContactFirstName { get; set; }
    public string ContactLastName { get; set; }
    public string ContactPhoneNumber { get; set; }
    public string Address { get; set; }
    public string Website { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string TaxAdministration { get; set; }
    public string TaxNumber { get; set; }
    public int CityId { get; set; }
    public int DistrictId { get; set; }
    public int? LogoId { get; set; }
    public bool IsSupplier { get; set; }
    public bool IsDeleted { get; set; }

    public virtual Supplier Supplier { get; set; }
    public virtual City City { get; set; }
    public virtual District District { get; set; }
    public virtual Document Logo { get; set; }
    public virtual ICollection<User> Users { get; set; }
    public virtual ICollection<Request> Requests { get; set; }
    public virtual ICollection<RequestGroup> RequestGroups { get; set; }

    public virtual ICollection<BankInfo> BankInfos { get; set; }
    public virtual ICollection<ApprovalChain> ApprovalChains { get; set; }
    public virtual ICollection<Budget> Budgets { get; set; }
    public virtual ICollection<Contract> Contracts { get; set; }
    public virtual ICollection<Department> Departments { get; set; }
    public virtual ICollection<Location> Locations { get; set; }
    public virtual ICollection<Category> Categories { get; set; }
    public virtual ICollection<OfferLimit> OfferLimits { get; set; }
    public virtual ICollection<Role> Roles { get; set; }
    public virtual ICollection<CompanySupplierPortfolio> SupplierPortfolios { get; set; }
    public virtual ICollection<Offer> Offers { get; set; }

    public virtual ICollection<CompanySubCategory> CompanySubCategories { get; set; }
    public virtual ICollection<CompanyRequestGroup> CompanyRequestGroups { get; set; }
    public virtual ICollection<SupplierAction> SupplierActions { get; set; }
    public virtual ICollection<OrderPreparation> OrderPreparations { get; set; }

}

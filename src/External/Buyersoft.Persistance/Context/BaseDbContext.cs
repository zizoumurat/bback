using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Buyersoft.Domain.Entitites;
using Buyersoft.Persistance.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Buyersoft.Domain.Entitites.Identity;
using System.Reflection.Emit;

namespace Buyersoft.Persistance.Context;

public class BaseDbContext : IdentityDbContext<User, Role, int>
{
    protected IConfiguration Configuration { get; set; }
    public DbSet<ApprovalChain> ApprovalChains { get; set; }
    public DbSet<ApprovalChainUser> ApprovalChainUsers { get; set; }
    public DbSet<Approval> Approvals { get; set; }
    public DbSet<BankInfo> BankInfos { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Budget> Budgets { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CategoryUser> CategoryUsers { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<ContractComment> ContractComments { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<CompanySupplierPortfolio> CompanySupplierPortfolios { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<CurrencyParameter> CurrencyParameters { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<MainCategory> MainCategories { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<OfferDetail> OfferDetails { get; set; }
    public DbSet<OfferLimit> OfferLimits { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderPreparation> OrderPreparations { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<RequestDocument> RequestDocuments { get; set; }
    public DbSet<CompanyRequestGroup> CompanyRequestGroups { get; set; }
    public DbSet<RequestGroup> RequestGroups { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<CompanySubCategory> CompanySubCategories { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<SupplierRequestGroup> SupplierRequestGroups { get; set; }
    public DbSet<SupplierRating> SupplierRatings { get; set; }
    public DbSet<SystemNotification> SystemNotifications { get; set; }
    public DbSet<TaxOffice> TaxOffices { get; set; }
    public DbSet<Template> Templates { get; set; }
    public DbSet<UserNotificationPreference> UserNotificationPreferences { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<ReverseAuction> ReverseAuctions { get; set; }
    public DbSet<ProductDefinition> ProductDefinitions { get; set; }
    public DbSet<ServiceDefinition> ServiceDefinitions { get; set; }
    public DbSet<SupplierAction> SupplierActions { get; set; }

    public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
    {
        Configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ApprovalChain>(ApprovalChainMapping.OnModelCreating);
        builder.Entity<ApprovalChainUser>(ApprovalChainUserMapping.OnModelCreating);
        builder.Entity<Approval>(ApprovalMapping.OnModelCreating);
        builder.Entity<BankInfo>(BankInfoMapping.OnModelCreating);
        builder.Entity<Branch>(BranchMapping.OnModelCreating);
        builder.Entity<Budget>(BudgetMapping.OnModelCreating);
        builder.Entity<Category>(CategoryMapping.OnModelCreating);
        builder.Entity<CategoryUser>(CategoryUserMapping.OnModelCreating);
        builder.Entity<City>(CityMapping.OnModelCreating);
        builder.Entity<Comment>(CommentMapping.OnModelCreating);
        builder.Entity<Company>(CompanyMapping.OnModelCreating);
        builder.Entity<Contract>(ContractMapping.OnModelCreating);
        builder.Entity<ContractComment>(ContractCommentMapping.OnModelCreating);
        builder.Entity<CompanySupplierPortfolio>(CompanySupplierPortfolioMapping.OnModelCreating);
        builder.Entity<Currency>(CurrencyMapping.OnModelCreating);
        builder.Entity<CurrencyParameter>(CurrencyParameterMapping.OnModelCreating);
        builder.Entity<Department>(DepartmentMapping.OnModelCreating);
        builder.Entity<District>(DistrictMapping.OnModelCreating);
        builder.Entity<Document>(DocumentMapping.OnModelCreating);
        builder.Entity<Location>(LocationMapping.OnModelCreating);
        builder.Entity<MainCategory>(MainCategoryMapping.OnModelCreating);
        builder.Entity<Notification>(NotificationMapping.OnModelCreating);
        builder.Entity<Offer>(OfferMapping.OnModelCreating);
        builder.Entity<OfferDetail>(OfferDetailMapping.OnModelCreating);
        builder.Entity<OfferLimit>(OfferLimitMapping.OnModelCreating);

        builder.Entity<Order>(OrderMapping.OnModelCreating);
        builder.Entity<OrderPreparation>(OrderPreparationMapping.OnModelCreating);
        builder.Entity<OrderItem>(OrderItemMapping.OnModelCreating);

        builder.Entity<RequestDocument>(RequestDocumentMapping.OnModelCreating);
        builder.Entity<Request>(RequestMapping.OnModelCreating);
        builder.Entity<CompanyRequestGroup>(CompanyRequestGroupMapping.OnModelCreating);
        builder.Entity<RequestGroup>(RequestGroupMapping.OnModelCreating);
        builder.Entity<Role>(RoleMapping.OnModelCreating);
        builder.Entity<CompanySubCategory>(CompanySubCategoryMapping.OnModelCreating);
        builder.Entity<SubCategory>(SubCategoryMapping.OnModelCreating);
        builder.Entity<Supplier>(SupplierMapping.OnModelCreating);
        builder.Entity<SupplierRequestGroup>(SupplierRequestGroupMapping.OnModelCreating);
        builder.Entity<SystemNotification>(SystemNotificationMapping.OnModelCreating);
        builder.Entity<TaxOffice>(TaxOfficeMapping.OnModelCreating);
        builder.Entity<Template>(TemplateMapping.OnModelCreating);
        builder.Entity<User>(UserMapping.OnModelCreating);
        builder.Entity<UserNotificationPreference>(UserNotificationPreferenceMapping.OnModelCreating);
        builder.Entity<Permission>(PermissionMapping.OnModelCreating);
        builder.Entity<ReverseAuction>(ReverseAuctionMapping.OnModelCreating);
        builder.Entity<RolePermission>();
        builder.Entity<ProductDefinition>(ProductDefinitionMapping.OnModelCreating);
        builder.Entity<ServiceDefinition>(ServiceDefinitionMapping.OnModelCreating);
        builder.Entity<SupplierAction>(SupplierActionMapping.OnModelCreating);
        RolePermissionMapping.OnModelCreating(builder.Entity<RolePermission>(), builder.Entity<Permission>());
    }
}
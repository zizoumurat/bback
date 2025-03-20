using Buyersoft.Application.Dtos;
using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// RolePermissionMapping.
/// </summary>

public static class RolePermissionMapping
{
    /// <summary>
    /// Called when /[model creating].
    /// </summary>
    /// <param name="builder">The builder.</param>
    public static void OnModelCreating(EntityTypeBuilder<RolePermission> builder, EntityTypeBuilder<Permission> permissionBuilder)
    {
        builder.ToTable("RolePermissions");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.RoleId).IsRequired();
        builder.Property(p => p.PermissionId).IsRequired();

        builder.HasOne(rp => rp.Role)
         .WithMany(r => r.RolePermissions)
         .HasForeignKey(rp => rp.RoleId);

        builder.HasOne(e => e.Permission)
        .WithMany(p => p.RolePermissions)
        .HasForeignKey(rp => rp.PermissionId);

        var permissions = GetSeedPermissions();
        permissionBuilder.HasData(permissions);

        var supplierPermissionNameList = GetSupplierPermissions();
        var supplierPermissions = permissions.Where(x => supplierPermissionNameList.Contains(x.Name)).ToList();
        var companyPermissions = permissions.Where(x => !supplierPermissionNameList.Contains(x.Name)).ToList();
        var keyUserPermissions = companyPermissions.Where(x => x.Name != "requests.creator");
        var requesterPermissions = companyPermissions.Where(x => x.Name == "requests.creator");

        int index = 0;

        List<RolePermission> rolePermissions = new List<RolePermission>();

        foreach (var permission in keyUserPermissions)
        {
            rolePermissions.Add(new RolePermission()
            {
                Id = ++index,
                PermissionId = permission.Id,
                RoleId = 1
            });
        }

        foreach (var permission in requesterPermissions)
        {
            rolePermissions.Add(new RolePermission()
            {
                Id = ++index,
                PermissionId = permission.Id,
                RoleId = 2
            });
        }

        foreach (var permission in supplierPermissions)
        {
            rolePermissions.Add(new RolePermission()
            {
                Id = ++index,
                PermissionId = permission.Id,
                RoleId = 3
            });
        }

        builder.HasData(rolePermissions);
    }

    public static List<ModuleDto> GetModulesWithPermissions()
    {
        string[] defaultPermissions = { "create", "read", "update", "delete" };

        List<ModuleDto> modules = new();

        #region adminpanel
        string moduleName = "adminPanel";

        ModuleDto adminPanelModule = new()
        {
            Name = moduleName,
            PermissionList = defaultPermissions.Select(permission => new PermissionDto
            {
                Name = $"{moduleName}.{permission}"
            }).ToList()
        };

        modules.Add(adminPanelModule);
        #endregion

        #region request
        moduleName = "requests";

        ModuleDto requestModule = new()
        {
            Name = moduleName,
            PermissionList = new()
            {
                new()
                {
                    Name = $"{moduleName}.creator"
                },
                new()
                {
                    Name = $"{moduleName}.owner"
                }
            }
        };

        modules.Add(requestModule);
        #endregion

        #region offer
        moduleName = "offers";

        string[] offerPermissions = {
            "requestSelection",
            "comparisonTable",
            "reverseOpenAuction",
            "reverseOpenAuctionList",
            "allocation",
            "bidCollectionProcessSummary",
            "currentRequests",
            "makeOffer",
            "offerHistory",
            "revisedOffersRequested",
            "reverseOpenAuctionListSupplier",
            "reverseOpenAuctionSupplier"
        };

        ModuleDto offerModule = new()
        {
            Name = moduleName,
            PermissionList = offerPermissions.Select(permission => new PermissionDto
            {
                Name = $"{moduleName}.{permission}"
            }).ToList()
        };

        modules.Add(offerModule);
        #endregion

        #region approval
        moduleName = "approvals";

        string[] approvalPermissions = {
            "pendingApprovalRequests",
            "approvalRequestsArchive",
            "approvalRequestDetail"
        };

        ModuleDto approvalModule = new()
        {
            Name = moduleName,
            PermissionList = approvalPermissions.Select(permission => new PermissionDto
            {
                Name = $"{moduleName}.{permission}"
            }).ToList()
        };

        modules.Add(approvalModule);
        #endregion

        #region contract
        moduleName = "contracts";

        string[] contractPermissions = {
            "pendingApprovalContracts",
            "contractsArchive",
        };

        ModuleDto contractModule = new()
        {
            Name = moduleName,
            PermissionList = contractPermissions.Select(permission => new PermissionDto
            {
                Name = $"{moduleName}.{permission}"
            }).ToList()
        };

        modules.Add(contractModule);
        #endregion

        #region orders
        moduleName = "orders";

        string[] orderPermissions = {
            "catalog",
            "orderLists",
            "orderArchive",
            "statusUpdate",
            "fourWayMatching",
        };

        ModuleDto orderModule = new()
        {
            Name = moduleName,
            PermissionList = orderPermissions.Select(permission => new PermissionDto
            {
                Name = $"{moduleName}.{permission}"
            }).ToList()
        };

        modules.Add(orderModule);
        #endregion

        #region suppliers
        moduleName = "suppliers";

        string[] supplierPermissions = {
            "supplierPortfolioManagement",
            "supplierPerformanceManagement",
        };

        ModuleDto supplierModule = new()
        {
            Name = moduleName,
            PermissionList = supplierPermissions.Select(permission => new PermissionDto
            {
                Name = $"{moduleName}.{permission}"
            }).ToList()
        };

        modules.Add(supplierModule);
        #endregion

        #region customers
        moduleName = "customers";

        string[] customerPermissions = {
            "customerPortfolioManagement",
            "customerPerformanceManagement",
            "receivables",
        };

        ModuleDto customerModule = new()
        {
            Name = moduleName,
            PermissionList = customerPermissions.Select(permission => new PermissionDto
            {
                Name = $"{moduleName}.{permission}"
            }).ToList()
        };

        modules.Add(customerModule);
        #endregion

        #region payments
        moduleName = "payments";

        string[] paymentPermissions = {
            "paymentLists",
            "paymentApprovals",
            "paymentInstructions",
        };

        ModuleDto paymentModule = new()
        {
            Name = moduleName,
            PermissionList = paymentPermissions.Select(permission => new PermissionDto
            {
                Name = $"{moduleName}.{permission}"
            }).ToList()
        };

        modules.Add(paymentModule);
        #endregion

        return modules;
    }

    public static List<Permission> GetSeedPermissions()
    {
        int idCounter = 1;
        var modules = GetModulesWithPermissions();

        var allPermissions = modules
            .SelectMany(module => module.PermissionList
                .Select(permission => new Permission()
                {
                    Id = idCounter++,
                    Name = permission.Name
                }))
            .Distinct()
            .ToList();

        return allPermissions;
    }

    public static List<string> GetSupplierPermissions()
    {
        var modules = GetModulesWithPermissions();

        var permissionsToAdd = new List<string>
        {
            "offers.currentRequests",
            "offers.makeOffer",
            "offers.offerHistory",
            "offers.revisedOffersRequested",
            "offers.reverseOpenAuctionListSupplier",
            "offers.reverseOpenAuctionSupplier"
        };

        var offerPermissions = modules
            .Where(module => module.Name == "suppliers")
            .SelectMany(module => module.PermissionList)
            .Distinct()
            .ToList();

        offerPermissions.AddRange(permissionsToAdd.Select(permission => new PermissionDto { Name = permission }));

        return offerPermissions.Select(x => x.Name).Distinct().ToList();
    }
}

using Buyersoft.Domain.Entitites.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// RoleMapping.
/// </summary>
public static class RoleMapping
{
    public static void OnModelCreating(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(r => r.IsSystemRole)
            .IsRequired();

        builder.Property(r => r.IsHiddenRole)
            .IsRequired();

        builder.HasOne(r => r.Company)
            .WithMany(c => c.Roles)
            .HasForeignKey(r => r.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(r => r.Users)
            .WithOne(u => u.Role)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        SeedData(builder);
    }

    private static void SeedData(EntityTypeBuilder<Role> builder)
    {
        builder.HasData(
           new Role
           {
               Id = 1,
               Name = "KeyUser",
               NormalizedName = "KEYUSER",
               CompanyId = null,
               IsSystemRole = true,
               IsHiddenRole = false,
           },
           new Role
           {
               Id = 2,
               Name = "Requestor",
               NormalizedName = "REQUESTOR",
               CompanyId = null,
               IsSystemRole = true,
               IsHiddenRole = false,
           },
           new Role
           {
               Id = 3,
               Name = "Supplier",
               NormalizedName = "SUPPLIER",
               CompanyId = null,
               IsSystemRole = true,
               IsHiddenRole = true,
           }
       );
    }
}

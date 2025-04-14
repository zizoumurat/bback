using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// DepartmentMapping.
/// </summary>
public static class DepartmentMapping
{
    public static void OnModelCreating(EntityTypeBuilder<Department> builder)
    {
        // Tablo adını belirtme
        builder.ToTable("Departments");

        // Primary Key belirtme
        builder.HasKey(d => d.Id);

        // Property konfigurasyonları
        builder.Property(d => d.Name).IsRequired().HasMaxLength(100);

        // İlişki konfigurasyonları
        builder.HasOne(d => d.Company)
               .WithMany(c => c.Departments)
               .HasForeignKey(d => d.CompanyId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(d => d.Budgets)
               .WithOne(b => b.Department)
               .HasForeignKey(b => b.DepartmentId)
               .OnDelete(DeleteBehavior.Restrict);

        SeedData(builder);
    }

    private static void SeedData(EntityTypeBuilder<Department> builder)
    {
        builder.HasData(
           new Department
           {
               Id = 1,
               Name = "Yönetim",
               CompanyId = 1,
           }
       );
    }
}



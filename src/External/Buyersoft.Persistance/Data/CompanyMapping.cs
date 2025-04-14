using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// CompanyMapping.
/// </summary>
public static class CompanyMapping
{
    public static void OnModelCreating(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
        builder.Property(e => e.Address).HasMaxLength(500);
        builder.Property(e => e.Email).HasMaxLength(100);
        builder.Property(e => e.Phone).HasMaxLength(20);
        builder.Property(e => e.TaxAdministration).HasMaxLength(100);
        builder.Property(e => e.TaxNumber).HasMaxLength(20);

        builder.HasOne(e => e.City)
            .WithMany(c => c.Companies)
            .HasForeignKey(e => e.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Logo)
            .WithOne(c => c.Company)
             .HasForeignKey<Company>(c => c.LogoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.District)
            .WithMany(d => d.Companies)
            .HasForeignKey(e => e.DistrictId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Departments)
             .WithOne(d => d.Company)
             .HasForeignKey(d => d.CompanyId)
             .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Users)
            .WithOne(u => u.Company)
            .HasForeignKey(u => u.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.BankInfos)
            .WithOne(bi => bi.Company)
            .HasForeignKey(bi => bi.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.ApprovalChains)
            .WithOne(ac => ac.Company)
            .HasForeignKey(ac => ac.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Budgets)
            .WithOne(b => b.Company)
            .HasForeignKey(b => b.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        SeedData(builder);
    }

    private static void SeedData(EntityTypeBuilder<Company> builder)
    {
        builder.HasData(
           new Company
           {
               Id = 1,
               Name = "Deneme Şirket",
               Address = "Adres",
               CityId = 34,
               DistrictId = 485,
           }
       );
    }
}

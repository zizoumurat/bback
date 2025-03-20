using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// CompanySupplierPortfolioMapping.
/// </summary>
public static class CompanySupplierPortfolioMapping
{
    public static void OnModelCreating(EntityTypeBuilder<CompanySupplierPortfolio> builder)
    {
        builder.ToTable("CompanySupplierPortfolios");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.CompanyId).IsRequired();
        builder.Property(e => e.SupplierId).IsRequired();


        builder.HasOne(e => e.Company)
            .WithMany(c => c.SupplierPortfolios)
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Supplier)
            .WithMany()
            .HasForeignKey(e => e.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// BankInfoMapping.
/// </summary>

public static class BankInfoMapping
{
    public static void OnModelCreating(EntityTypeBuilder<BankInfo> builder)
    {
        builder.ToTable("BankInfos");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.IBAN).HasMaxLength(34).IsRequired();
        builder.Property(e => e.CompanyId).IsRequired();
        builder.Property(e => e.CurrencyId).IsRequired();
        builder.Property(e => e.BranchId).IsRequired(false);

        builder.HasOne(e => e.Company)
                .WithMany(c => c.BankInfos)
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Currency)
            .WithMany(c => c.BankInfos)
            .HasForeignKey(e => e.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Branch)
            .WithMany(b => b.BankInfos)
            .HasForeignKey(e => e.BranchId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

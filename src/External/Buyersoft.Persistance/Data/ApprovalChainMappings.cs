using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// ApprovalChainMapping.
/// </summary>

public static class ApprovalChainMapping
{
    /// <summary>
    /// Called when /[model creating].
    /// </summary>
    /// <param name="builder">The builder.</param>
    public static void OnModelCreating(EntityTypeBuilder<ApprovalChain> builder)
    {
        builder.ToTable("ApprovalChains");

        builder.HasKey(ac => ac.Id);

        builder.Property(ac => ac.CurrencyId).IsRequired();
        builder.Property(ac => ac.CompanyId).IsRequired();
        builder.Property(e => e.SpendLimit).HasPrecision(18, 2).IsRequired();

        builder.HasOne(e => e.Company)
                .WithMany(c => c.ApprovalChains)
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Currency)
            .WithMany(c => c.ApprovalChains)
            .HasForeignKey(e => e.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

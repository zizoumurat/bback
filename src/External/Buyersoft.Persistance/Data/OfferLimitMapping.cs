using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// OfferLimitMapping.
/// </summary>
public static class OfferLimitMapping
{
    public static void OnModelCreating(EntityTypeBuilder<OfferLimit> builder)
    {
        builder.ToTable("OfferLimits");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.CompanyId).IsRequired();
        builder.Property(e => e.CurrencyId).IsRequired();
        builder.Property(e => e.SpendLimit).IsRequired().HasPrecision(18, 4);
        builder.Property(e => e.MinimumOfferCount).IsRequired();

        builder.HasOne(e => e.Company)
            .WithMany(c => c.OfferLimits)
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Currency)
            .WithMany(c => c.OfferLimits)
            .HasForeignKey(e => e.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}



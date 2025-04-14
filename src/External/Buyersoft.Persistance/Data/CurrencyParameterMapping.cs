using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// CurrencyParameterMapping.
/// </summary>
public static class CurrencyParameterMapping
{
    public static void OnModelCreating(EntityTypeBuilder<CurrencyParameter> builder)
    {
        builder.ToTable("CurrencyParameters");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.ExchangeRate).HasPrecision(18, 4).IsRequired();
        builder.Property(e => e.StartDate).IsRequired();
        builder.Property(e => e.ExpiredDate).IsRequired();

        builder.HasOne(e => e.Currency1)
            .WithMany(c => c.ExchangeRatesCurrency1)
            .HasForeignKey(e => e.Currency1Id)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Currency2)
            .WithMany(c => c.ExchangeRatesCurrency2)
            .HasForeignKey(e => e.Currency2Id)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Company)
            .WithMany()
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}



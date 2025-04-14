using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// CurrencyMapping.
/// </summary>
public static class CurrencyMapping
{
    public static void OnModelCreating(EntityTypeBuilder<Currency> builder)
    {
        builder.ToTable("Currencies");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Code).HasMaxLength(10).IsRequired();
        builder.Property(e => e.Name).HasMaxLength(50).IsRequired();

        builder.HasMany(e => e.BankInfos)
            .WithOne(b => b.Currency)
            .HasForeignKey(bi => bi.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.ApprovalChains)
            .WithOne(ac => ac.Currency)
            .HasForeignKey(ac => ac.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.ExchangeRatesCurrency1)
            .WithOne(cp => cp.Currency1)
            .HasForeignKey(cp => cp.Currency1Id)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.ExchangeRatesCurrency2)
            .WithOne(cp => cp.Currency2)
            .HasForeignKey(cp => cp.Currency2Id)
            .OnDelete(DeleteBehavior.Restrict);

        SeedData(builder);
    }

    private static void SeedData(EntityTypeBuilder<Currency> builder)
    {
        int id = 0;
        builder.HasData(
           new Currency
           {
               Id = ++id,
               Code = "TRY",
               Name = "Türk Lirası"
           },
           new Currency
           {
               Id = ++id,
               Code = "EUR",
               Name = "Euro"
           },
           new Currency
           {
               Id = ++id,
               Code = "USD",
               Name = "Dollar"
           },
           new Currency
           {
               Id = ++id,
               Code = "SAR",
               Name = "Riyal"
           },
           new Currency
           {
               Id = ++id,
               Code = "BMB",
               Name = "Bambu"
           },
           new Currency
           {
               Id = ++id,
               Code = "GBP",
               Name = "Sterlin"
           },
           new Currency
           {
               Id = ++id,
               Code = "JPY",
               Name = "Japon Yeni"
           },
           new Currency
           {
               Id = ++id,
               Code = "CHF",
               Name = "İsviçre Frangı"
           },
           new Currency
           {
               Id = ++id,
               Code = "CNY",
               Name = "Çin Yuanı"
           }
       );
    }
}


using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// TaxOfficeMapping.
/// </summary>
public static class TaxOfficeMapping
{
    public static void OnModelCreating(EntityTypeBuilder<TaxOffice> builder)
    {
        builder.ToTable("TaxOffices");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(e => e.Code)
            .IsRequired();

        builder.HasOne(e => e.City)
            .WithMany(x => x.TaxOffices)
            .HasForeignKey(e => e.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        SeedData(builder);
    }

    private static void SeedData(EntityTypeBuilder<TaxOffice> builder)
    {
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var seedDataPath = Path.Combine(basePath, "SeedData", "TaxOffices.json");
        var seedData = File.ReadAllText(seedDataPath);
        var taxOffices = JsonSerializer.Deserialize<List<TaxOffice>>(seedData);

        foreach (var taxOffice in taxOffices)
        {
            builder.HasData(taxOffice);
        }
    }
}

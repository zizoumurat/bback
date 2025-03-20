using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// DistrictMapping.
/// </summary>
public static class DistrictMapping
{
    public static void OnModelCreating(EntityTypeBuilder<District> builder)
    {
        // Tablo adını belirtme
        builder.ToTable("Districts");

        // Primary Key belirtme
        builder.HasKey(d => d.Id);

        // Property konfigurasyonları
        builder.Property(d => d.Name).IsRequired().HasMaxLength(200);

        // İlişki konfigurasyonları
        builder.HasOne(d => d.City)
               .WithMany(c => c.Districts)
               .HasForeignKey(d => d.CityId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(d => d.Branch)
               .WithOne(b => b.District)
               .HasForeignKey(b => b.DistrictId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Locations)
                .WithOne(l => l.District)
                .HasForeignKey(l => l.DistrictId);

        SeedData(builder);
    }

    private static void SeedData(EntityTypeBuilder<District> builder)
    {
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var seedDataPath = Path.Combine(basePath, "SeedData", "Districts.json");
        var seedData = File.ReadAllText(seedDataPath);
        var districts = JsonSerializer.Deserialize<List<District>>(seedData);

        foreach (var district in districts)
        {
            builder.HasData(district);
        }
    }
}



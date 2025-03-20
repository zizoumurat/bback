using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// CityMapping.
/// </summary>

public static class CityMapping
{
    public static void OnModelCreating(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("Cities");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).HasMaxLength(100).IsRequired();

        builder.HasMany(e => e.Branches)
            .WithOne(b => b.City)
            .HasForeignKey(b => b.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Districts)
            .WithOne(d => d.City)
            .HasForeignKey(d => d.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Locations)
            .WithOne(l => l.City)
            .HasForeignKey(l => l.CityId);

        SeedData(builder);
    }

    private static void SeedData(EntityTypeBuilder<City> builder)
    {
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var seedDataPath = Path.Combine(basePath, "SeedData", "Cities.json");
         var seedData = File.ReadAllText(seedDataPath);
        var cities =  JsonSerializer.Deserialize<List<City>>(seedData);

        foreach (var city in cities)
        {
            builder.HasData(city);
        }
    }
}

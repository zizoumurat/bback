using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Net;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// LocationMapping.
/// </summary>
public static class LocationMapping
{
    public static void OnModelCreating(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("Locations");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.CompanyId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(255);
        builder.Property(e => e.Address).IsRequired().HasMaxLength(500);
        builder.Property(e => e.CityId).IsRequired();
        builder.Property(e => e.DistrictId).IsRequired();

        builder.HasOne(e => e.Company)
            .WithMany(c=> c.Locations)
            .HasForeignKey(c => c.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.City)
            .WithMany(l=> l.Locations)
            .HasForeignKey(l => l.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.District)
            .WithMany(l => l.Locations)
            .HasForeignKey(e => e.DistrictId)
            .OnDelete(DeleteBehavior.Restrict);

        SeedData(builder);
    }

    private static void SeedData(EntityTypeBuilder<Location> builder)
    {
        builder.HasData(
           new Location
           {
               Id = 1,
               CompanyId = 1,
               Name = "Ýstanbul Location",
               Address = "Adres bilgisi",
               CityId = 34,
               DistrictId = 485
           }
       );
    }
}

using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// BrancheMapping.
/// </summary>

public static class BranchMapping
{
    public static void OnModelCreating(EntityTypeBuilder<Branch> builder)
    {
        builder.ToTable("Branches");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.BankName).HasMaxLength(100).IsRequired();
        builder.Property(e => e.BranchName).HasMaxLength(100).IsRequired();
        builder.Property(e => e.Address).HasMaxLength(500).IsRequired();
        builder.Property(e => e.PhoneNumber).HasMaxLength(100).IsRequired();
        builder.Property(e => e.FaksNumber).HasMaxLength(100);
        builder.Property(e => e.CityId).IsRequired();
        builder.Property(e => e.DistrictId).IsRequired();

        builder.HasOne(e => e.City)
            .WithMany(c => c.Branches)
            .HasForeignKey(e => e.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.District)
            .WithMany(d => d.Branch)
            .HasForeignKey(e => e.DistrictId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.BankInfos)
            .WithOne(bi => bi.Branch)
            .HasForeignKey(bi => bi.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

        SeedData(builder);
    }

    private static void SeedData(EntityTypeBuilder<Branch> builder)
    {
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var seedDataPath = Path.Combine(basePath, "SeedData", "Branches.json");
        var seedData = File.ReadAllText(seedDataPath);
        var branches = JsonSerializer.Deserialize<List<Branch>>(seedData);
        int id = 0;

        foreach (var branch in branches)
        {
            branch.Id = ++id;
            branch.PhoneNumber = branch.PhoneNumber ?? "";
            branch.Address = branch.Address ?? "";
            builder.HasData(branch);
        }
    }
}


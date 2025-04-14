using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using System.Text.Json;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// RequestGroupMapping.
/// </summary>
public static class RequestGroupMapping
{
    public static void OnModelCreating(EntityTypeBuilder<RequestGroup> builder)
    {
        builder.HasKey(rg => rg.Id);

        builder.Property(rg => rg.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(rg => rg.SubCategoryId)
            .IsRequired();

        builder.HasOne(e => e.SubCategory)
            .WithMany(mc => mc.RequestGroups)
            .HasForeignKey(e => e.SubCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(rg => rg.Categories)
            .WithOne(c => c.RequestGroup)
            .HasForeignKey(c => c.RequestGroupId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.SupplierRequestGroups)
         .WithOne(srg => srg.RequestGroup)
         .HasForeignKey(srg => srg.RequestGroupId)
         .OnDelete(DeleteBehavior.Cascade);

        SeedData(builder);
    }

    private static void SeedData(EntityTypeBuilder<RequestGroup> builder)
    {
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var seedDataPath = Path.Combine(basePath, "SeedData", "RequestGroups.json");
        var seedData = File.ReadAllText(seedDataPath);
        var models = JsonSerializer.Deserialize<List<RequestGroup>>(seedData);
        int id = 0;

        foreach (var model in models)
        {
            model.Id = ++id;
            builder.HasData(model);
        }
    }
}



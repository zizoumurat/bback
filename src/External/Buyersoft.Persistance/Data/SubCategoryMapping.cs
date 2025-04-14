using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// SubCategoryMapping.
/// </summary>
public static class SubCategoryMapping
{
    public static void OnModelCreating(EntityTypeBuilder<SubCategory> builder)
    {
        builder.ToTable("SubCategories");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(e => e.MainCategoryId)
            .IsRequired();

        builder.HasOne(e => e.MainCategory)
            .WithMany(mc => mc.SubCategories)
            .HasForeignKey(e => e.MainCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Categories)
            .WithOne(c => c.SubCategory)
            .HasForeignKey(c => c.SubCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        SeedData(builder);
    }

    private static void SeedData(EntityTypeBuilder<SubCategory> builder)
    {
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var seedDataPath = Path.Combine(basePath, "SeedData", "SubCategories.json");
        var seedData = File.ReadAllText(seedDataPath);
        var subCategories = JsonSerializer.Deserialize<List<SubCategory>>(seedData);
        int id = 0;
        foreach (var subCategory in subCategories)
        {
            subCategory.Id = ++id;
            builder.HasData(subCategory);
        }
    }
}

public class SubCategoryModel
{
    public string Name { get; set; }
    public int MainCategoryId { get; set; }
}

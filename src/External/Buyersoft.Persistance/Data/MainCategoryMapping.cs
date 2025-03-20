using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// MainCategoryMapping.
/// </summary>
public static class MainCategoryMapping
{
    public static void OnModelCreating(EntityTypeBuilder<MainCategory> builder)
    {
        builder.ToTable("MainCategories");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);

        builder.HasMany(e => e.Categories)
            .WithOne(c => c.MainCategory)
            .HasForeignKey(c => c.MainCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.SubCategories)
            .WithOne(sc => sc.MainCategory)
            .HasForeignKey(sc => sc.MainCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        SeedData(builder);
    }

    private static void SeedData(EntityTypeBuilder<MainCategory> builder)
    {
        builder.HasData(
           new MainCategory
           {
               Id = 1,
               Name = "Ürün",
           },
           new MainCategory
           {
               Id = 2,
               Name = "Hizmet",
           }
       );
    }
}
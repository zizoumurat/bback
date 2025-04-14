using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// CategoryMapping.
/// </summary>

public static class CategoryMapping
{
    public static void OnModelCreating(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.CompanyId).IsRequired();
        builder.Property(e => e.MainCategoryId).IsRequired();
        builder.Property(e => e.SubCategoryId).IsRequired();
        builder.Property(e => e.RequestGroupId).IsRequired();
        builder.Property(e => e.LocationId).IsRequired();
        builder.Property(e => e.LeadTime).IsRequired();
        builder.Property(e => e.Unit).IsRequired().HasMaxLength(20);

        builder.HasOne(e => e.Company)
            .WithMany(c => c.Categories)
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.MainCategory)
            .WithMany(m => m.Categories)
            .HasForeignKey(e => e.MainCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.SubCategory)
            .WithMany(s => s.Categories)
            .HasForeignKey(e => e.SubCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Location)
            .WithMany(l => l.Categories)
            .HasForeignKey(e => e.LocationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

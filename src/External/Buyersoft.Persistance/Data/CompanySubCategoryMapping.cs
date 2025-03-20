using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public static class CompanySubCategoryMapping
{
    public static void OnModelCreating(EntityTypeBuilder<CompanySubCategory> builder)
    {
        builder.ToTable("CompanySubCategories");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(e => e.CompanyId)
            .IsRequired();

        builder.Property(e => e.SubCategoryId)
            .IsRequired();

        builder.HasOne(e => e.Company)
            .WithMany(c => c.CompanySubCategories)
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.SubCategory)
            .WithOne(sc => sc.CompanySubCategory)
            .HasForeignKey<CompanySubCategory>(e => e.SubCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// ProductDefinitionMapping.
/// </summary>

public static class ProductDefinitionMapping
{
    public static void OnModelCreating(EntityTypeBuilder<ProductDefinition> builder)
    {
        builder.ToTable("ProductDefinitions");

        builder.HasKey(e => e.Id);

        builder.Property(p => p.Code)
           .IsRequired()
           .HasMaxLength(50);

        builder.Property(p => p.Definition)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(p => p.Category)
             .WithMany(c => c.ProductDefinitions)  
             .HasForeignKey(p => p.CategoryId)    
             .OnDelete(DeleteBehavior.Restrict);
    }
}

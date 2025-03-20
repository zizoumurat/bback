using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// CategoryUserMapping.
/// </summary>

public static class CategoryUserMapping
{
    public static void OnModelCreating(EntityTypeBuilder<CategoryUser> builder)
    {
        builder.ToTable("CategoryUsers");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.UserId).IsRequired();
        builder.Property(e => e.CategoryId).IsRequired();

        builder.HasOne(cu => cu.Category)
            .WithMany(c => c.CategoryUsers)
            .HasForeignKey(cu => cu.CategoryId);

        builder.HasOne(cu => cu.User)
            .WithMany(u => u.CategoryUsers)
            .HasForeignKey(cu => cu.UserId);
    }
}

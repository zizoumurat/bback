using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// SupplierActionMapping.
/// </summary>

public static class SupplierActionMapping
{
    /// <summary>
    /// Called when /[model creating].
    /// </summary>
    /// <param name="builder">The builder.</param>
    public static void OnModelCreating(EntityTypeBuilder<SupplierAction> builder)
    {
        builder.ToTable("SupplierActions");

        builder.HasKey(ac => ac.Id);

        builder.Property(ac => ac.Subject).IsRequired().HasMaxLength(200);
        builder.Property(ac => ac.Detail).HasMaxLength(500);
        builder.Property(ac => ac.SupplierNotes).HasMaxLength(500);
        builder.Property(ac => ac.DueDate).IsRequired();

        builder.HasOne(e => e.Company)
                .WithMany(c => c.SupplierActions)
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Supplier)
            .WithMany(c => c.SupplierActions)
            .HasForeignKey(e => e.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.User)
            .WithMany(x=>x.SupplierActions)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

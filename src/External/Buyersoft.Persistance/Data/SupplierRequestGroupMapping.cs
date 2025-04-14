using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// SupplierRequestGroupMapping.
/// </summary>
public static class SupplierRequestGroupMapping
{
    public static void OnModelCreating(EntityTypeBuilder<SupplierRequestGroup> builder)
    {
        builder.ToTable("SupplierRequestGroups");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.RequestGroupId).IsRequired();
        builder.Property(e => e.SupplierId).IsRequired();

        builder.HasOne(e => e.Supplier)
        .WithMany(p => p.SupplierRequestGroups)
        .HasForeignKey(rp => rp.SupplierId);

        builder.HasOne(e => e.RequestGroup)
        .WithMany(p => p.SupplierRequestGroups)
        .HasForeignKey(rp => rp.RequestGroupId);
    }
}

using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// SupplierMapping.
/// </summary>
public static class SupplierMapping
{
    public static void OnModelCreating(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable("Suppliers");

        builder.HasKey(e => e.Id);

        builder.HasMany(e => e.SupplierRatings)
        .WithOne(p => p.Supplier)
        .HasForeignKey(rp => rp.SupplierId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Company)
       .WithOne(c => c.Supplier)  // 1:1 ilişki burada tanımlanıyor
       .HasForeignKey<Supplier>(s => s.CompanyId)
       .OnDelete(DeleteBehavior.Restrict); // Silme davranışı
    }
}

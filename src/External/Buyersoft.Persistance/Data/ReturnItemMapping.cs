using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

public static class ReturnItemMapping
{
    public static void OnModelCreating(EntityTypeBuilder<ReturnItem> builder)
    {
        builder.ToTable("ReturnItems");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.ReturnId).IsRequired();
        builder.Property(e => e.OrderItemId).IsRequired();
        builder.Property(e => e.TotalPrice).IsRequired().HasPrecision(18, 4);
        builder.Property(e => e.UnitPrice).IsRequired().HasPrecision(18, 4);
        builder.Property(e => e.Quantity).IsRequired();

        builder.HasOne(e => e.Return)
            .WithMany(c => c.ReturnItems)
            .HasForeignKey(e => e.ReturnId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.OrderItem)
            .WithOne(c => c.ReturnItem)
            .HasForeignKey<ReturnItem>(e => e.OrderItemId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}




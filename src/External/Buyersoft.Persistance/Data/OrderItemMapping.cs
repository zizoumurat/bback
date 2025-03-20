using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

public static class OrderItemMapping
{
    public static void OnModelCreating(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.OrderId).IsRequired();
        builder.Property(e => e.OfferDetailId).IsRequired();
        builder.Property(e => e.ProductDefinition).IsRequired().HasMaxLength(50);
        builder.Property(e => e.TotalPrice).IsRequired().HasPrecision(18, 4);
        builder.Property(e => e.UnitPrice).IsRequired().HasPrecision(18, 4);
        builder.Property(e => e.Quantity).IsRequired();

        builder.HasOne(e => e.Order)
            .WithMany(c => c.OrderItems)
            .HasForeignKey(e => e.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.OfferDetail)
            .WithMany(c => c.OrderItems)
            .HasForeignKey(e => e.OfferDetailId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}



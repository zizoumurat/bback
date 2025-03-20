using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

public static class OrderMapping
{
    public static void OnModelCreating(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.OrderPreparationId).IsRequired();
        builder.Property(e => e.OrderCode).IsRequired().HasMaxLength(40);
        builder.Property(e => e.TotalPrice).IsRequired().HasPrecision(18, 4);
        builder.Property(e => e.OrderDate).IsRequired();


        builder.HasOne(e => e.OrderPreparation)
            .WithMany(c => c.Orders)
            .HasForeignKey(e => e.OrderPreparationId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}



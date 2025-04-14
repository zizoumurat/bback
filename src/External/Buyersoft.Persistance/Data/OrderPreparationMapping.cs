using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

public static class OrderPreparationMapping
{
    public static void OnModelCreating(EntityTypeBuilder<OrderPreparation> builder)
    {
        builder.ToTable("OrderPreparations");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.CompanyId).IsRequired();
        builder.Property(e => e.MainCategory).IsRequired().HasMaxLength(50);
        builder.Property(e => e.SubCategory).IsRequired().HasMaxLength(40);
        builder.Property(e => e.RequestGroup).IsRequired().HasMaxLength(40);
        builder.Property(e => e.TotalPrice).IsRequired().HasPrecision(18, 4);

        builder.HasOne(e => e.Request)
            .WithMany(c => c.OrderPreparations)
            .HasForeignKey(e => e.RequestId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Company)
            .WithMany(c => c.OrderPreparations)
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Offer)
            .WithMany(c => c.OrderPreparations)
            .HasForeignKey(e => e.OfferId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}



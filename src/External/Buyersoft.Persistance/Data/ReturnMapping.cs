using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

public static class ReturnMapping
{
    public static void OnModelCreating(EntityTypeBuilder<Return> builder)
    {
        builder.ToTable("Returns");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.OrderId).IsRequired();
        builder.Property(e => e.InvoiceNumber).IsRequired().HasMaxLength(40);
        builder.Property(e => e.WaybillNumber).IsRequired().HasMaxLength(40);
        builder.Property(e => e.Reason).IsRequired().HasMaxLength(400);
        builder.Property(e => e.TotalPrice).IsRequired().HasPrecision(18, 4);


        builder.HasOne(e => e.Order)
            .WithMany(c => c.Returns)
            .HasForeignKey(e => e.OrderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}




using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

public static class PaymentListMapping
{
    public static void OnModelCreating(EntityTypeBuilder<PaymentList> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.CompanyId).IsRequired();
        builder.Property(e => e.PaymentListCode).IsRequired().HasMaxLength(40);
        builder.Property(e => e.Subject).IsRequired().HasMaxLength(40);
        builder.Property(e => e.Status).IsRequired();
        builder.Property(e => e.TotalPrice).IsRequired().HasPrecision(18, 4);

        builder.HasOne(o => o.Company)
            .WithMany(d => d.PaymentLists)
            .HasForeignKey(d => d.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
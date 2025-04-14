using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

public static class PaymentListApprovalMapping
{
    public static void OnModelCreating(EntityTypeBuilder<PaymentListApproval> builder)
    {
        builder.ToTable("PaymentListApprovals");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.PaymentListId).IsRequired();
        builder.Property(e => e.UserId).IsRequired();
        builder.Property(e => e.Status).IsRequired();
        builder.Property(e => e.Comment).HasMaxLength(400);

        builder.HasOne(e => e.PaymentList)
            .WithMany(o => o.PaymentListApprovals)
            .HasForeignKey(e => e.PaymentListId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}


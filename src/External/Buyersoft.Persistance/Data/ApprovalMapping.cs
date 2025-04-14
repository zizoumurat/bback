using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// ApprovalMapping.
/// </summary>

public static class ApprovalMapping
{
    public static void OnModelCreating(EntityTypeBuilder<Approval> builder)
    {
        builder.ToTable("Approvals");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.RequestId).IsRequired();
        builder.Property(e => e.UserId).IsRequired();
        builder.Property(e => e.Status).IsRequired();
        builder.Property(e => e.Comment).HasMaxLength(400);

        builder.HasOne(e => e.Request)
            .WithMany(o => o.Approvals)
            .HasForeignKey(e => e.RequestId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

public static class ContractApprovalMapping
{
    public static void OnModelCreating(EntityTypeBuilder<ContractApproval> builder)
    {
        builder.ToTable("ContractApprovals");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.ContractId).IsRequired();
        builder.Property(e => e.UserId).IsRequired();
        builder.Property(e => e.Status).IsRequired();
        builder.Property(e => e.Comment).HasMaxLength(400);

        builder.HasOne(e => e.Contract)
            .WithMany(o => o.ContractApprovals)
            .HasForeignKey(e => e.ContractId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}


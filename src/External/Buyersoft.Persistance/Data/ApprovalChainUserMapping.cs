using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// ApprovalChainUserMapping.
/// </summary>

public static class ApprovalChainUserMapping
{
    public static void OnModelCreating(EntityTypeBuilder<ApprovalChainUser> builder)
    {
        builder.ToTable("ApprovalChainUsers");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.ApprovalChainId).IsRequired();
        builder.Property(e => e.UserId).IsRequired();

        builder.HasOne(acu => acu.User)
            .WithMany(u => u.ApprovalChainUsers)
            .HasForeignKey(acu => acu.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(acu => acu.ApprovalChain)
            .WithMany(ac => ac.ApprovalChainUsers)
            .HasForeignKey(acu => acu.ApprovalChainId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}



using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// ReverseAuctionMapping.
/// </summary>

public static class ReverseAuctionMapping
{
    public static void OnModelCreating(EntityTypeBuilder<ReverseAuction> builder)
    {
        builder.ToTable("ReverseAuctions");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.StartTime).IsRequired();
        builder.Property(e => e.EndTime).IsRequired();
        builder.Property(e => e.RequestId).IsRequired();
        builder.Property(e => e.MeetLink).IsRequired().HasMaxLength(120);

        builder.HasOne(cu => cu.Request)
              .WithOne(c => c.ReverseAuction) 
              .HasForeignKey<ReverseAuction>(ra => ra.RequestId);
    }
}

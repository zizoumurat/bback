using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// OfferMapping.
/// </summary>
public static class OfferMapping
{
    public static void OnModelCreating(EntityTypeBuilder<Offer> builder)
    {
        builder.ToTable("Offers");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.CompanyId).IsRequired();
        builder.Property(e => e.ReferenceCode).IsRequired().HasMaxLength(50);
        builder.Property(e => e.TotalPrice).IsRequired().HasPrecision(18, 4);
        builder.Property(e => e.AverageUnitPrice).IsRequired().HasPrecision(18, 4);
        builder.Property(e => e.RequestId).IsRequired();
        builder.Property(e => e.MaturityDays).IsRequired();
        builder.Property(e => e.OfferStatus).IsRequired();
        builder.Property(e => e.RejectionReason).HasMaxLength(400);

        builder
             .HasOne(o => o.RevisedOffer)
             .WithOne()
             .HasForeignKey<Offer>(o => o.RevisedOfferId)
             .OnDelete(DeleteBehavior.Restrict); 

        builder
            .HasOne(o => o.OriginalOffer) 
            .WithMany()
            .HasForeignKey(o => o.OriginalOfferId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Company)
            .WithMany(u => u.Offers)
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Request)
            .WithMany(u => u.Offers)
            .HasForeignKey(e => e.RequestId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Contract)
            .WithOne(u => u.Offer)
            .HasForeignKey<Contract>(e => e.OfferId);
    }
}

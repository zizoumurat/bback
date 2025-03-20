using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

public static class OfferDetailMapping
{
    public static void OnModelCreating(EntityTypeBuilder<OfferDetail> builder)
    {
        builder.ToTable("OfferDetails");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.OfferId).IsRequired();
        builder.Property(e => e.ProductDefinition).IsRequired().HasMaxLength(250);
        builder.Property(e => e.Quantity).IsRequired();
        builder.Property(e => e.UnitPrice).IsRequired().HasPrecision(18, 4);
        builder.Property(e => e.FirstUnitPrice).IsRequired().HasPrecision(18, 4);
        builder.Property(e => e.TotalPrice).IsRequired().HasPrecision(18, 4);

        builder.HasOne(o => o.Offer)
            .WithMany(d => d.OfferDetails)
            .HasForeignKey(d => d.OfferId)
            .OnDelete(DeleteBehavior.Restrict);
    }

}

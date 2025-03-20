using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

public static class ContractMapping
{
    public static void OnModelCreating(EntityTypeBuilder<Contract> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.CompanyId).IsRequired();
        builder.Property(e => e.OfferId).IsRequired();
        builder.Property(e => e.RequestId).IsRequired();
        builder.Property(e => e.ContractStatus).IsRequired();
        builder.Property(e => e.StartDate).IsRequired();
        builder.Property(e => e.ExpirationDate).IsRequired();
        builder.Property(e => e.ReferenceCode).IsRequired().HasMaxLength(20);
        builder.Property(e => e.ReferenceCode).HasMaxLength(400);
        builder.Property(e => e.TotalPrice).IsRequired().HasPrecision(18, 4);

        builder.HasOne(o => o.Company)
            .WithMany(d => d.Contracts)
            .HasForeignKey(d => d.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.Request)
            .WithMany(d => d.Contracts)
            .HasForeignKey(d => d.RequestId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}


public static class ContractCommentMapping
{
    public static void OnModelCreating(EntityTypeBuilder<ContractComment> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.ContractId).IsRequired();
        builder.Property(e => e.UserId).IsRequired();
        builder.Property(e => e.Comment).IsRequired().HasMaxLength(400);


        builder.HasOne(o => o.Contract)
            .WithMany(d => d.ContractComments)
            .HasForeignKey(d => d.ContractId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.User)
            .WithMany(d => d.ContractComments)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}

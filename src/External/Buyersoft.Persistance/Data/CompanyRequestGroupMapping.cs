using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public static class CompanyRequestGroupMapping
{
    public static void OnModelCreating(EntityTypeBuilder<CompanyRequestGroup> builder)
    {
        builder.ToTable("CompanyRequestGroups");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(e => e.CompanyId)
            .IsRequired();

        builder.Property(e => e.RequestGroupId)
            .IsRequired();

        builder.HasOne(e => e.Company)
            .WithMany(c => c.CompanyRequestGroups)
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

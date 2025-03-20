using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// TemplateMapping.
/// </summary>
public static class TemplateMapping
{
    public static void OnModelCreating(EntityTypeBuilder<Template> builder)
    {
        builder.ToTable("Templates");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(e => e.Data)
            .IsRequired();

        builder.HasMany(e => e.Requests)
            .WithOne(r => r.Template)
            .HasForeignKey(r => r.TemplateId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

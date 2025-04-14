using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

public static class ServiceDefinitionMapping
{
    public static void OnModelCreating(EntityTypeBuilder<ServiceDefinition> builder)
    {
        builder.ToTable("ServiceDefinitions");

        builder.HasKey(e => e.Id);

        builder.Property(p => p.Definition)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(p => p.Category)
             .WithMany(c => c.ServiceDefinitions)
             .HasForeignKey(p => p.CategoryId)
             .OnDelete(DeleteBehavior.Restrict);
    }
}


using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// RequestDocumentMapping.
/// </summary>
public static class RequestDocumentMapping
{
    public static void OnModelCreating(EntityTypeBuilder<RequestDocument> builder)
    {
        builder.ToTable("RequestDocuments");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.RequestId).IsRequired();
        builder.Property(e => e.DocumentId).IsRequired();

        builder.HasOne(rd => rd.Request)
            .WithMany(r => r.RequestDocuments)
            .HasForeignKey(rd => rd.RequestId)
            .OnDelete(DeleteBehavior.Restrict); 

        builder.HasOne(rd => rd.Document)
            .WithMany(d => d.RequestDocuments)
            .HasForeignKey(rd => rd.DocumentId)
            .OnDelete(DeleteBehavior.Restrict); 
    }
}

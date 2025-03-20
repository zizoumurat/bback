using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// DocumentMapping.
/// </summary>
public static class DocumentMapping
{
    public static void OnModelCreating(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable("Documents");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.FileName).IsRequired().HasMaxLength(200);
        builder.Property(d => d.FileContent).IsRequired().HasColumnType("varbinary(max)");
        builder.Property(d => d.FileType).IsRequired().HasMaxLength(100);
        builder.Property(d => d.UploadDate).IsRequired();
    }
}


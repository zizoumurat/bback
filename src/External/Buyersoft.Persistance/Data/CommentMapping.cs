using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// CommentMapping.
/// </summary>

public static class CommentMapping
{
    public static void OnModelCreating(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.UserId).IsRequired();
        builder.Property(e => e.CommentText).HasMaxLength(1000).IsRequired();
        builder.Property(e => e.RequestId).IsRequired();

        builder.HasOne(e => e.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Request)
            .WithMany(r => r.Comments)
            .HasForeignKey(e => e.RequestId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

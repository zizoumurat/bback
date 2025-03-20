using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// NotificationMapping.
/// </summary>
public static class NotificationMapping
{
    public static void OnModelCreating(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("Notifications");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.UserId).IsRequired();
        builder.Property(e => e.Message).IsRequired().HasMaxLength(500);
        builder.Property(e => e.Read).IsRequired();

        builder.HasOne(e => e.User)
            .WithMany(u => u.Notifications)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// SystemNotificationMapping.
/// </summary>
public static class SystemNotificationMapping
{
    public static void OnModelCreating(EntityTypeBuilder<SystemNotification> builder)
    {
        builder.ToTable("SystemNotifications");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.Message)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(e => e.Type)
            .IsRequired()
            .HasMaxLength(20);
    }
}

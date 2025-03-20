using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// UserNotificationPreferenceMapping.
/// </summary>
public static class UserNotificationPreferenceMapping
{
    public static void OnModelCreating(EntityTypeBuilder<UserNotificationPreference> builder)
    {
        builder.ToTable("UserNotificationPreferences");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.UserId)
            .IsRequired();

        builder.Property(e => e.Method)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(e => e.User)
            .WithMany(u => u.NotificationPreferences)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

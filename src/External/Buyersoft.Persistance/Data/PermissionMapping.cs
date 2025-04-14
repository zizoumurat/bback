using Buyersoft.Application.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// PermissionMapping.
/// </summary>

public static class PermissionMapping
{
    /// <summary>
    /// Called when /[model creating].
    /// </summary>
    /// <param name="builder">The builder.</param>
    public static void OnModelCreating(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissions");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name).IsRequired();
    }
}

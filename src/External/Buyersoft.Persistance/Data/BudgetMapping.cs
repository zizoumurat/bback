using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// BudgetMapping.
/// </summary>

public static class BudgetMapping
{
    public static void OnModelCreating(EntityTypeBuilder<Budget> builder)
    {
        builder.ToTable("Budgets");

        var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(
            d => d.ToDateTime(TimeOnly.MinValue),
            d => DateOnly.FromDateTime(d));


        builder.HasKey(e => e.Id);

        builder.Property(e => e.CompanyId).IsRequired();
        builder.Property(e => e.CurrencyId).IsRequired();
        builder.Property(e => e.StartDate).HasConversion(dateOnlyConverter).IsRequired();
        builder.Property(e => e.EndDate).HasConversion(dateOnlyConverter).IsRequired();
        builder.Property(e => e.DepartmentId).IsRequired();
        builder.Property(e => e.BudgetLimit).HasPrecision(18, 2).IsRequired();
        builder.Property(e => e.AvailableLimit).HasPrecision(18, 2).IsRequired();
        builder.Property(e => e.BudgetTitle).HasMaxLength(100).IsRequired();

        builder.HasOne(e => e.Company)
            .WithMany(c => c.Budgets)
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Department)
            .WithMany(d => d.Budgets)
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Currency)
            .WithMany(c => c.Budgets)
            .HasForeignKey(e => e.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Requests)
            .WithOne(r => r.Budget)
            .HasForeignKey(r => r.BudgetId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}



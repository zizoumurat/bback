using Buyersoft.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// RequestMapping.
/// </summary>
public static class RequestMapping
{
    public static void OnModelCreating(EntityTypeBuilder<Request> builder)
    {
        builder.ToTable("Requests");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title).IsRequired().HasMaxLength(255);
        builder.Property(e => e.Amount).HasPrecision(18, 2).IsRequired();
        builder.Property(e => e.RequestedSupplyDate).IsRequired();
        builder.Property(e => e.EstimatedSupplyDate);
        builder.Property(e => e.Reason).IsRequired();
        builder.Property(e => e.State).IsRequired();

        builder.HasOne(e => e.Currency)
            .WithMany()
            .HasForeignKey(e => e.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict); 

        builder.HasOne(e => e.Template)
            .WithMany()
            .HasForeignKey(e => e.TemplateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.CreatedBy)
            .WithMany(x => x.CreatedRequests)
            .HasForeignKey(e => e.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Manager)
            .WithMany(x => x.ManagedRequests)
            .HasForeignKey(e => e.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);  
        
        builder.HasOne(e => e.Budget)
            .WithMany(b => b.Requests)
            .HasForeignKey(e => e.BudgetId)
            .OnDelete(DeleteBehavior.Restrict);  
        
        builder.HasOne(e => e.Category)
            .WithMany(b => b.Requests)
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Company)
            .WithMany(u => u.Requests)
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }

}

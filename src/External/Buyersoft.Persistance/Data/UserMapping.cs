using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Entitites.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buyersoft.Persistance.Data;

/// <summary>
/// UserMapping.
/// </summary>
public static class UserMapping
{
    public static void OnModelCreating(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.RoleId).IsRequired();
        builder.Property(e => e.CompanyId).IsRequired();
        builder.Property(e => e.DepartmentId).IsRequired();

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(e => e.Surname)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(e => e.Title).HasMaxLength(255);

        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(e => e.PasswordHash).IsRequired(false);

        builder.Property(e => e.ChoosenLanguage).HasMaxLength(10);

        builder.HasOne(e => e.UserPhoto)
            .WithOne(e=>e.User)
            .HasForeignKey<User>(c => c.UserPhotoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Company)
            .WithMany(c => c.Users)
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne(e => e.Department)
            .WithMany(d => d.Users)
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(e => e.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.ApprovalChainUsers)
            .WithOne(acu => acu.User)
            .HasForeignKey(acu => acu.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        SeedData(builder);
    }


    private static void SeedData(EntityTypeBuilder<User> builder)
    {
        var hasher = new PasswordHasher<User>();

        builder.HasData(
            new User
            {
                Id = 1,
                UserName = "ahmet.yilmaz",
                NormalizedUserName = "AHMET.YILMAZ",
                Email = "keyuser@buyersoft.com",
                NormalizedEmail = "KEYUSER@BUYERSOFT.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Pswrd!5."),
                SecurityStamp = Guid.NewGuid().ToString("D"),
                RoleId = 1,
                CompanyId = 1,
                DepartmentId = 1,
                Name = "Ahmet",
                Surname = "Yılmaz",
                Title = "Genel Müdür",
            }
       );
    }
}


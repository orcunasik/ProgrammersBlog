using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgrammersBlog.Entities.Concrete;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Picture).IsRequired().HasMaxLength(300);
        // Primary key
        builder.HasKey(u => u.Id);

        builder.HasIndex(u => u.NormalizedUserName).HasDatabaseName("UserNameIndex").IsUnique();
        builder.HasIndex(u => u.NormalizedEmail).HasDatabaseName("EmailIndex");

        builder.ToTable("AspNetUsers");

        builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

        builder.Property(u => u.UserName).HasMaxLength(50);
        builder.Property(u => u.NormalizedUserName).HasMaxLength(50);
        builder.Property(u => u.Email).HasMaxLength(100);
        builder.Property(u => u.NormalizedEmail).HasMaxLength(100);

        builder.HasMany<UserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
        builder.HasMany<UserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();
        builder.HasMany<UserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();
        builder.HasMany<UserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();

        User adminUser = new User
        {
            Id = 1,
            UserName = "adminuser",
            NormalizedUserName = "ADMINUSER",
            Email = "adminuser@gmail.com",
            NormalizedEmail = "ADMINUSER@GMAIL.COM",
            PhoneNumber = "+905555555555",
            Picture = "userImages/defaultUser.png",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        adminUser.PasswordHash = CreatePasswordHash(adminUser,"adminuser");

        User editorUser = new User
        {
            Id = 2,
            UserName = "editoruser",
            NormalizedUserName = "EDITORUSER",
            Email = "editoruser@gmail.com",
            NormalizedEmail = "EDITORUSER@GMAIL.COM",
            PhoneNumber = "+905555555555",
            Picture = "userImages/defaultUser.png",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        editorUser.PasswordHash = CreatePasswordHash(editorUser, "editoruser");

        builder.HasData(adminUser, editorUser);
    }
    private string CreatePasswordHash(User user, string password)
    {
        PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
        return passwordHasher.HashPassword(user, password);
    }
}
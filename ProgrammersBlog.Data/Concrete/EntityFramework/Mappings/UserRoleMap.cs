﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgrammersBlog.Entities.Concrete;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Mappings;

public class UserRoleMap : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(r => new { r.UserId, r.RoleId });

        builder.ToTable("AspNetUserRoles");

        builder.HasData(
            new UserRole
            {
                RoleId = 1,
                UserId = 1
            },
            new UserRole
            {
                RoleId = 2,
                UserId = 2
            });
    }
}
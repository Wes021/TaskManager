using Identity.Identity.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Identity.Infrastructure.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
            new Role
            {
                Id = 1,
                Name = "Employee",
                IsActive = true,
                IsDeleted = false
            },
            new Role
            {
                Id = 2,
                Name = "Admin",
                IsActive = true,
                IsDeleted = false
            },
            new Role
            {
                Id = 3,
                Name = "ManagerAndLeader",
                IsActive = true,
                IsDeleted = false
            }
            
            
            
            );
            
        }
    }
}

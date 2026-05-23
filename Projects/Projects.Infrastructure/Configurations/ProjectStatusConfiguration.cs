using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projects.Projects.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Projects.Infrastructure.Configurations
{
    public class ProjectStatusConfiguration : IEntityTypeConfiguration<ProjectStatus>
    {
        public void Configure(EntityTypeBuilder<ProjectStatus> builder)
        {
            builder.HasData(
            new ProjectStatus
            {
                Id = 1,
                Name = "Draft",
                IsActive = true,
                IsDeleted = false
            },
            new ProjectStatus
            {
                Id = 2,
                Name = "Active",
                IsActive = true,
                IsDeleted = false
            },
            new ProjectStatus
            {
                Id = 3,
                Name = "OnHold",
                IsActive = true,
                IsDeleted = false
            },
            new ProjectStatus
            {
                Id = 4,
                Name = "Completed",
                IsActive = true,
                IsDeleted = false
            },
            new ProjectStatus
            {
                Id = 5,
                Name = "Cancelled",
                IsActive = true,
                IsDeleted = false
            }

            );

        }
    }
}

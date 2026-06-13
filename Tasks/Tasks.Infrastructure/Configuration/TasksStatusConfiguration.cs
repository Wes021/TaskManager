using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasks.Tasks.Domain.Models;

namespace Tasks.Tasks.Infrastructure.Configuration
{
    public class TasksStatusConfiguration
    {
        public void Configure(EntityTypeBuilder<TasksStatus> builder)
        {
            builder.HasData(
            new TasksStatus
            {
                Id = 1,
                Name = "Draft",
                IsDeleted = false
            },
            new TasksStatus
            {
                Id = 2,
                Name = "Active",

                IsDeleted = false
            },
            new TasksStatus
            {
                Id = 3,
                Name = "Completed",

                IsDeleted = false
            },
            new TasksStatus
            {
                Id = 4,
                Name = "Cancelled",

                IsDeleted = false
            }

            );
        }
    }
}

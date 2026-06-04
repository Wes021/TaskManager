using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Tasks.Domain.Models;

namespace Tasks.Tasks.Infrastructure.DbSettings
{
    public class TasksDbContext : DbContext
    {
        public TasksDbContext(DbContextOptions<TasksDbContext> options)
                  : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
        typeof(TasksDbContext).Assembly);

            base.OnModelCreating(modelBuilder);



        }

        
        public DbSet<TaskAttachments> TaskAttachments { get; set; }
        public DbSet<Tasks.Domain.Models.Tasks> Task { get; set; }
        public DbSet<TasksStatus> TasksStatus { get; set; }
        public DbSet<UsersTasks> UsersTasks { get; set; }
    }
}

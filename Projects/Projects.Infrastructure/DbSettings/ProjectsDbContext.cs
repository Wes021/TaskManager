using Microsoft.EntityFrameworkCore;
using Projects.Projects.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Projects.Infrastructure.DbSettings
{
    public class ProjectsDbContext : DbContext
    {
        public ProjectsDbContext(DbContextOptions<ProjectsDbContext> options)
                  : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
        typeof(ProjectsDbContext).Assembly);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProjectMember>()
    .HasKey(x => new { x.ProjectId, x.UserId });
        }



        public DbSet<Project> Project { get; set; }
        public DbSet<ProjectMember> ProjectMember { get; set; }
        public DbSet<ProjectStatus> ProjectStatus { get; set; }
    }
}

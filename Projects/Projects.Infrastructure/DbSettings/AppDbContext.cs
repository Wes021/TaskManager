using Microsoft.EntityFrameworkCore;
using Projects.Projects.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Projects.Infrastructure.DbSettings
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
                  : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }



        public DbSet<Project> Project { get; set; }
        public DbSet<ProjectMember> ProjectMember { get; set; }
        public DbSet<ProjectStatus> ProjectStatus { get; set; }
    }
}

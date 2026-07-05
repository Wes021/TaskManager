using Microsoft.EntityFrameworkCore;

namespace Teams.Teams.Infrastructure.DbSettings
{
    public class TeamsDbContext : DbContext
    {
        public TeamsDbContext(DbContextOptions<TeamsDbContext> options)
                  : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
        typeof(TeamsDbContext).Assembly);

            base.OnModelCreating(modelBuilder);



        }

    }
}

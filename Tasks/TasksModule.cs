using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Projects.Infrastructure.DbSettings;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Projects
{
    public static class TasksModule
    {
        public static IServiceCollection AddProjectsModule(this IServiceCollection services, IConfiguration configuration)
        {

            //Repositories:
            


            //Serives
          


            //Handlers
           



            services.AddDbContext<ProjectsDbContext>(options =>
          options.UseSqlServer(configuration.GetConnectionString("SqlCon")));

            services.AddAutoMapper(typeof(TasksModule).Assembly);
            return services;
        }
    }
}

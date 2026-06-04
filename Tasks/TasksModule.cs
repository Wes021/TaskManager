using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Tasks.Infrastructure.DbSettings;

namespace Projects
{
    public static class TasksModule
    {
        public static IServiceCollection AddTasksModule(this IServiceCollection services, IConfiguration configuration)
        {

            //Repositories:
            


            //Serives
          


            //Handlers
           



            services.AddDbContext<TasksDbContext>(options =>
          options.UseSqlServer(configuration.GetConnectionString("SqlCon")));

            services.AddAutoMapper(typeof(TasksModule).Assembly);
            return services;
        }
    }
}

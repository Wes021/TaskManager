using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.SharedLayer.Interfaces;
using Tasks.Tasks.Domain.Services.Services;
using Tasks.Tasks.Infrastructure.DbSettings;

namespace Projects
{
    public static class TasksModule
    {
        public static IServiceCollection AddTasksModule(this IServiceCollection services, IConfiguration configuration)
        {

            //Repositories:



            //Serives
            services.AddScoped<IFileManager, FileManager>();


            //Handlers




            services.AddDbContext<TasksDbContext>(options =>
          options.UseSqlServer(configuration.GetConnectionString("SqlCon")));

            services.AddAutoMapper(typeof(TasksModule).Assembly);
            return services;
        }
    }
}

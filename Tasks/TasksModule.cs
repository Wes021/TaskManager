using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tasks.Tasks.Application.Handlers.Handlers;
using Tasks.Tasks.Application.Handlers.IHandlers;
using Tasks.Tasks.Domain.IRepositories;
using Tasks.Tasks.Domain.IUnitOfWork;
using Tasks.Tasks.Domain.Services.IServices;
using Tasks.Tasks.Domain.Services.Services;
using Tasks.Tasks.Infrastructure.DbSettings;
using Tasks.Tasks.Infrastructure.Repositories;
using Tasks.Tasks.Infrastructure.UnitOfWork;

namespace Projects
{
    public static class TasksModule
    {
        public static IServiceCollection AddTasksModule(this IServiceCollection services, IConfiguration configuration)
        {

            //Repositories:
            services.AddScoped<ITasksRepository, TasksRepository>();
            services.AddScoped<ITaskStatusRepository, TaskStatusRepository>();
            services.AddScoped<ITasksModuleUoW, TasksModuleUoW>();
            services.AddScoped<IUsersTasks, UsersTasks>();



            //Serives
            services.AddScoped<ITasksService, TasksService>();
            services.AddScoped<ITaskComments, TaskComments>();


            //Handlers
            services.AddScoped<ITasksHandler, TasksHandler>();
            services.AddScoped<ITaskCommentsHandler, TaskCommentsHandler>();



            services.AddDbContext<TasksDbContext>(options =>
          options.UseSqlServer(configuration.GetConnectionString("SqlCon")));

            services.AddAutoMapper(typeof(TasksModule).Assembly);
            return services;
        }
    }
}

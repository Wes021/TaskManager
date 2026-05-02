using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Projects.Projects.Application.Handlers.Handler;
using Projects.Projects.Application.Handlers.IHandler;
using Projects.Projects.Domain.IRepositories;
using Projects.Projects.Domain.IUnitOfWork;
using Projects.Projects.Domain.Services.IServices;
using Projects.Projects.Domain.Services.Services;
using Projects.Projects.Infrastructure.Repositories;
using Projects.Projects.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects
{
    public static class ProjectsModule
    {
        public static IServiceCollection AddProjectsModule(this IServiceCollection services, IConfiguration configuration)
        {

            //Repositories:
            services.AddScoped<IProjectsRepository, ProjectsRepository>();
            services.AddScoped<IProjectStatusRepository, ProjectStatusRepository>();
            services.AddScoped<IProjectModuleUoW, ProjectModuleUoW>();


            //Serives
            services.AddScoped<IProjectService, ProjectService>();


            //Handlers
            services.AddScoped<IProjectHandler, ProjectHandler>();


            return services;
        }
    }
}

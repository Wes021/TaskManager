using Identity;
using Projects;
namespace TaskManager

{
    public static class DependencyInjection
    {
        public static IServiceCollection AddModules(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddIdentityModule(configuration);
            services.AddProjectsModule(configuration);
            services.AddTasksModule(configuration);



            return services;
        }
    }
}

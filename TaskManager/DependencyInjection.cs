using Identity;
namespace TaskManager

{
    public static class DependencyInjection
    {
        public static IServiceCollection AddModules(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddIdentityModule(configuration);



            return services;
        }
    }
}

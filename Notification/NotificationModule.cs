using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notification.Notification.Application.Handlers.Handlers;
using Notification.Notification.Application.Handlers.IHandlers;
using Notification.Notification.Domain.IRepositories;
using Notification.Notification.Domain.IUnitOfWork;
using Notification.Notification.Domain.Services.IServices;
using Notification.Notification.Domain.Services.Services;
using Notification.Notification.Infrastructure.DbSettings;
using Notification.Notification.Infrastructure.Repositories;
using Notification.Notification.Infrastructure.UnitOfWork;
namespace Notification
{
    public static class NotificationModule
    {
        public static IServiceCollection AddNotificationModule(this IServiceCollection services, IConfiguration configuration)
        {

            //Repositories:
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<INotificationsModuleUoW, NotificationsModuleUoW>();



            //Serives
            services.AddScoped<IInternalNotificationService, InternalNotificationService>();
            services.AddScoped<INotificationService, NotificationService>();



            //Handlers
            services.AddScoped<INotificationsHandlers, NotificationsHandlers>();


            services.AddDbContext<NotificationDbContext>(options =>
options.UseSqlServer(configuration.GetConnectionString("SqlCon")));

            services.AddAutoMapper(typeof(NotificationModule).Assembly);
            return services;
        }
    }
}

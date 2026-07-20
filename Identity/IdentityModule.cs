using Identity.Identity.Application.Handlers.Handlers;
using Identity.Identity.Application.Handlers.IHandlers;
using Identity.Identity.Domain.IRepositories;
using Identity.Identity.Domain.IUnitOfWork;
using Identity.Identity.Domain.Services.IServices;
using Identity.Identity.Domain.Services.Services;
using Identity.Identity.Infrastructure.Repositories;
using Identity.Identity.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Identity.Domain.IRepositories;
using Module.Identity.Domain.Services.IServices;
using Module.Identity.Domain.Services.Services;
using Module.Identity.Infrastructure.DbSettings;
using Module.Identity.Infrastructure.Repositories;
using TaskManager.SharedLayer.Interfaces;

namespace Identity
{
    public static class IdentityModule
    {
        public static IServiceCollection AddIdentityModule(this IServiceCollection services, IConfiguration configuration)
        {
            //Repositories:
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IIdentityMouduleUoW, IdentityMouduleUoW>();
            services.AddScoped<IGeneratedOTPRepository, GeneratedOTPRepository>();


            //Serives
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IJwtService, JwtService>();
            // services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IManageUserService, ManageUserService>();
            services.AddScoped<IUserLookupService, UserLookupService>();
            services.AddScoped<IGenerateOtpService, GenerateOtpService>();
            services.AddScoped<IOtpService, OtpService>();

            //Handlers
            services.AddScoped<ILoginHandler, LoginHandler>();
            services.AddScoped<IProfileHandler, ProfileHandler>();
            services.AddScoped<IUsersHandlers, UsersHandlers>();
            services.AddScoped<IOTPHandler, OTPHandler>();


            services.AddDbContext<IdentityDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SqlCon")));

            services.AddAutoMapper(typeof(IdentityModule).Assembly);
            return services;
        }
    }
}

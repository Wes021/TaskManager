using Identity.Identity.Application.Handlers.Handlers;
using Identity.Identity.Application.Handlers.IHandlers;
using Identity.Identity.Domain.Services.IServices;
using Identity.Identity.Domain.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Identity.Domain.IRepositories;
using Module.Identity.Domain.Services.IServices;
using Module.Identity.Domain.Services.Services;
using Module.Identity.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity
{
    public static class IdentityModule
    {
        public static IServiceCollection AddIdentityModule(this IServiceCollection services, IConfiguration configuration)
        {
            //Repositories:
            services.AddScoped<IUserRepository, UserRepository>();
            

            //Serives
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IManageUserService, ManageUserService>();

            //Handlers
            services.AddScoped<ILoginHandler, LoginHandler>();
            services.AddScoped<IProfileHandler, ProfileHandler>();
            services.AddScoped<IUsersHandlers, UsersHandlers>();


            services.AddAutoMapper(typeof(IdentityModule).Assembly);
            return services;
        }
    }
}

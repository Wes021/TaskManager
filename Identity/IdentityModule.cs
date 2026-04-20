using Identity.Identity.Application.Handlers.Handlers;
using Identity.Identity.Application.Handlers.IHandlers;
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


            //Handlers
            services.AddScoped<ILoginHandler, LoginHandler>();
            

            return services;
        }
    }
}

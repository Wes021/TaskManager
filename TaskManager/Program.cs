using Identity.Identity.Domain.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Projects.Projects.Domain.Services.Services;
using Resend;
using System.Security.Claims;
using System.Text;
using TaskManager.Middleware;
using TaskManager.SharedLayer.Enums;
using TaskManager.SharedLayer.Immplementaion;
using TaskManager.SharedLayer.Interfaces;
using TaskManager.SharedLayer.RequestModels.Identity;
using Tasks.Tasks.Infrastructure.Services;


namespace TaskManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddResend(options =>
            {
                options.ApiToken = builder.Configuration["ResendEmaillAPI:ApiKey"]!;
            });
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
            builder.Services.AddScoped<IProjectLookupService, ProjectLookupService>();
            builder.Services.AddScoped<IFileManager, FileManager>();
            builder.Services.AddScoped<IProjectAuthorizationService, ProjectAuthorizationService>();

            builder.Services.AddHttpClient<ResendClient>();
            builder.Services.AddTransient<IResend, ResendClient>();
            builder.Services.AddScoped<IEmailService, EmailService>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Enter: Bearer {your JWT token}"
                });

                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
            });
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddModules(builder.Configuration);


            builder.Services.AddLocalization();
            //        builder.Services.AddDbContext<IdentityDbContext>(options =>
            //options.UseSqlServer(

            //    builder.Configuration.GetConnectionString("SqlCon")));
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.ResetPassword, policy =>
                {
                    policy.RequireClaim(
                        "purpose",
                        ((int)SystemEnums.PolicyKeywords.PasswordReset).ToString());
                });
            });


            // JWT Settings
            var jwtSettings = builder.Configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    RoleClaimType = ClaimTypes.Role
                };
            });



            builder.Services.AddRateLimiter(options =>
            {
                options.AddSlidingWindowLimiter("Global", limiter =>
                {
                    limiter.PermitLimit = 100;
                    limiter.Window = TimeSpan.FromMinutes(1);
                    limiter.SegmentsPerWindow = 6;
                    limiter.QueueLimit = 0;
                });

                // Login Rate Limiter policy
                options.AddTokenBucketLimiter("Login", limiter =>
                {
                    limiter.TokenLimit = 5;
                    limiter.TokensPerPeriod = 1;
                    limiter.ReplenishmentPeriod = TimeSpan.FromSeconds(12);
                    limiter.AutoReplenishment = true;
                    limiter.QueueLimit = 0;
                });

                // OTP Rate Limiter policy
                options.AddFixedWindowLimiter("Otp", limiter =>
                {
                    limiter.PermitLimit = 3;
                    limiter.Window = TimeSpan.FromMinutes(10);
                    limiter.QueueLimit = 0;
                });

                // OTP Rate Limiter policy
                options.AddFixedWindowLimiter("VerifyOtp", limiter =>
                {
                    limiter.PermitLimit = 3;
                    limiter.Window = TimeSpan.FromMinutes(10);
                    limiter.QueueLimit = 0;
                });

                // Update Password Rate Limiter policy
                options.AddFixedWindowLimiter("UpdatePassword", limiter =>
                {
                    limiter.PermitLimit = 3;
                    limiter.Window = TimeSpan.FromMinutes(10);
                    limiter.QueueLimit = 0;
                });


            });


            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            var app = builder.Build();







            //Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

            }



            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();





            app.UseRateLimiter();

            app.MapControllers()
               .RequireRateLimiting("Global");

            app.Run();
        }
    }
}

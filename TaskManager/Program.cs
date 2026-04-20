
using Module.Identity.Infrastructure.DbSettings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.OpenApi.Models;
namespace TaskManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddModules(builder.Configuration);

            builder.Services.AddLocalization();
            builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(

        builder.Configuration.GetConnectionString("SqlCon")));

            var app = builder.Build();

            //Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
                
            //}
            
                app.UseSwagger();
                app.UseSwaggerUI();
                
            

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

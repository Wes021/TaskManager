using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using TaskManager.SharedLayer.ResponseModel;

namespace TaskManager.SharedLayer.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;


        public GlobalExceptionMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Unhandled exception. Path: {Path}",
                    context.Request.Path);


                await HandleExceptionAsync(context);
            }
        }




        private async Task HandleExceptionAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode =
                (int)HttpStatusCode.InternalServerError;


            var response = new ResponseModel<object>
            {
                Success = false,
                Data = null,
                Message = "SomthingWentWrong"
            };


            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}

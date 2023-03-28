using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Http;

namespace API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate request;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment environment;
        public ExceptionMiddleware(RequestDelegate request, ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
        {
            this.environment = environment;
            this.logger = logger;
            this.request = request;

        }

        public async Task InvokeAsync(Microsoft.AspNetCore.Http.HttpContext context)
        {
            try
            {
                await request(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                 var response = environment.IsDevelopment()
                    ? new ApiException(statusCode: (int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new ApiException((int)HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
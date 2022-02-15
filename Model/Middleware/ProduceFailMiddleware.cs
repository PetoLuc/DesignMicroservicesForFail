using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Routing;
using System.Net;

namespace Common.Middleware
{
    //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-6.0
    public class ProduceFailMiddleware
    {
        private readonly RequestDelegate _next;
        private static bool enabled = false;

        public ProduceFailMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {                        
            if (httpContext.Request.Path.Value.EndsWith("/fail"))
            {
                string firstParam = httpContext.Request.Query.FirstOrDefault().Key;
                switch (firstParam)
                {
                    case "enable":
                        {
                            enabled = true;

                        }
                        break;
                    case "disable":
                        {
                            enabled = false;
                        }
                        break;

                }
                await httpContext.Response.WriteAsync(enabled ? "enabled" : "disabled");
                return;
            }
            if (enabled)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;                
                return;
            }
            await _next(httpContext);
        }
    }
    //register
    public static class ProduceFailMiddlewareExtensions
    {
        public static IApplicationBuilder UseProduceFailMiddleware(this IApplicationBuilder builder)
        {
            var middleBuilder = new RouteBuilder(builder);
            return builder.UseMiddleware<ProduceFailMiddleware>();
        }
    }
}

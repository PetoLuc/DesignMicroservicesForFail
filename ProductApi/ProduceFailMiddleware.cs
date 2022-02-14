using System.Net;

namespace Producer_ProductApi
{
    public  class ProduceFailMiddleware
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
                await httpContext.Response.WriteAsync("Fial produced by middleware");
                return;
            }
            await _next(httpContext);
        }
    }
    public static class ProduceFailMiddlewareExtensions
    {
        public static IApplicationBuilder UseProduceFail(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ProduceFailMiddleware>();
        }
    }
}

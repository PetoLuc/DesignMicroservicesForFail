using Common.HealthCheck;
using Common.Middleware;
using HealthChecks.UI.Client;

using ProductApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRouting();
#region registerpossibilities
//app.UseEndpoints(endpoints => 
//{ 
//    endpoints.MapHealthChecks("/hcJson", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions() 
//    {
//        Predicate= _ => true,
//        ResponseWriter= UIResponseWriter.WriteHealthCheckUIResponse 
//    });
//    endpoints.MapHealthChecks("/hc", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
//    {        
//    });
//});
//app.MapHealthChecks("/healthz");
//app.MapHealthChecks("/hc/ready", new HealthCheckOptions
//{
//    Predicate = healthCheck => healthCheck.Tags.Contains("ready")
//});
#endregion registerpossibilities
//use healthcheck as jsom output
app.UseHealthChecks("/hc-json", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
//use simple healthcheck
app.UseHealthChecks("/hc");

app.MapControllers();
app.UseProduceFailMiddleware();
app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.AddScoped<IProductService, ProductService>();
    services.AddControllers();
    //register health check methods
    services.AddHealthChecks()
    .AddCheck("up", () => new Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult(Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Healthy, "OK"))
    .AddCheck("random fault",   new HealthCheckRandomFail(), Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy, new string[] { "does not matter" });
}
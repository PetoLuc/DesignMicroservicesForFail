using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Producer_ProductApi;
using ProductApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRouting();

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

app.UseHealthChecks("/hcJson", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.UseHealthChecks("/hc", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
{
});

app.UseHealthChecksUI(config => { config.UIPath = "/hc-ui"; config.ApiPath = "/hc-api"; }) ;

app.MapControllers();
app.UseProduceFail();
app.Run();

void ConfigureServices(IServiceCollection services)
{    
    services.AddScoped<IProductService, ProductService>();        
    services.AddControllers();
    services.AddHealthChecks()
    .AddCheck("randomfail", () =>
    {
        return Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Healthy();
    })
    .AddCheck("always OK", new HealthCheckAlwaysOK(), Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy, new string[] { "does not matter" });
    services.AddHealthChecksUI().AddInMemoryStorage();
}
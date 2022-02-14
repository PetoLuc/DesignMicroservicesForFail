using HttpClientTyped;
using OrderApi;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureServices(builder.Services);
var app = builder.Build();

app.MapControllers();
// Configure the HTTP request pipeline.


app.Run();

void ConfigureServices(IServiceCollection services)
{
    var productApiEndpointAddress = new Uri("http://localhost:5166");

    services.AddHttpClient("ProductApiClient1", c => { c.BaseAddress = productApiEndpointAddress; })
        .AddPolicyHandler(GetRetryPolicy()); 

    services.AddHttpClient<IProductServiceClient, ProductServiceClient>(c =>  {c.BaseAddress = productApiEndpointAddress;})    
    .AddPolicyHandler(GetCircuitBreakerPolicy());

    services.AddControllers();
}

static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
    var delay = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromSeconds(1), retryCount: 2);
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
        .WaitAndRetryAsync(/*6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,retryAttempt))*/ delay);
}

static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
{    
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .CircuitBreakerAsync(2, TimeSpan.FromSeconds(10));
}